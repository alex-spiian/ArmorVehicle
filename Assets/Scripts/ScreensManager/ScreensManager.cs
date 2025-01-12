using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ScreensManager
{
    [RequireComponent(typeof(ScreensRegister))]
    public class ScreensManager : MonoBehaviour
    {
        private static Dictionary<Type, UIScreenSettings> _screenRegistry = new();
        private static readonly Stack<UIScreenSettings> _screenStack = new();

        public void Initialize()
        {
            var screensRegister = GetComponent<ScreensRegister>();
            _screenRegistry = screensRegister.GetScreenRegistry();
            
            InitializeScreens();
        }

        private void InitializeScreens()
        {
            foreach (var keyValuePair in _screenRegistry)
            {
                keyValuePair.Value.Screen.Initialize(keyValuePair.Value.Guid);
                keyValuePair.Value.Screen.Closed += OnScreenClosed;
                keyValuePair.Value.Screen.Opened += OnScreenOpened;
                
                if (keyValuePair.Value.IsFirstScreen || keyValuePair.Value.IsUnClosable)
                {
                    Debug.Log($"{keyValuePair.Value.Guid} screen added to the stack.");
                    _screenStack.Push(keyValuePair.Value);
                    keyValuePair.Value.Screen.Open();
                    continue;
                }

                keyValuePair.Value.Screen.Close();
            }
        }

        private void OnDestroy()
        {
            foreach (var keyValuePair in _screenRegistry)
            {
                keyValuePair.Value.Screen.Closed -= OnScreenClosed;
                keyValuePair.Value.Screen.Opened -= OnScreenOpened;
            }
        }

        #region Public API
        
        public static void OpenScreen<UIScreenT, TContext>(TContext screenContext) where UIScreenT : UIScreen
        {
            ExecuteOnScreen<UIScreenT>(screenSettings =>
            {
                screenSettings.Screen.Open();
                screenSettings.Screen.Tick(screenContext);
                if (!_screenStack.Contains(screenSettings))
                {
                    Debug.Log($"{screenSettings.Guid} screen added to the stack.");
                    _screenStack.Push(screenSettings);
                }
            });
        }
        
        public static void OpenScreenAsync<UIScreenT, TContext>(TContext screenContext, UniTaskCompletionSource<OpenLevelType> taskCompletionSource) where UIScreenT : UIScreen
        {
            ExecuteOnScreen<UIScreenT>(screenSettings =>
            {
                screenSettings.Screen.Open();
                screenSettings.Screen.Tick(screenContext, taskCompletionSource);
                if (!_screenStack.Contains(screenSettings))
                {
                    Debug.Log($"{screenSettings.Guid} screen added to the stack.");
                    _screenStack.Push(screenSettings);
                }
            });
        }

        public static void CloseScreen<UIScreenT>() where UIScreenT : UIScreen
        {
            ExecuteOnScreen<UIScreenT>(screenSettings =>
            {
                if (screenSettings.IsUnClosable)
                    return;
                
                screenSettings.Screen.Close();
                if (_screenStack.Peek() == screenSettings)
                {
                    Debug.Log($"{screenSettings.Guid} screen removed from the stack.");
                    _screenStack.Pop();
                }
            });
        }

        public static void Back()
        {
            if (_screenStack.Count > 1)
            {
                var currentScreenSettings = _screenStack.Peek();
                if (currentScreenSettings.IsUnClosable)
                    return;
                    
                _screenStack.Pop();
                currentScreenSettings.Screen.Close();

                var previousScreenSettings = _screenStack.Peek();
                previousScreenSettings.Screen.Open();
            }
            else
            {
                Debug.LogWarning("No previous screen to return to.");
            }
        }

        public static void CloseAllScreens()
        {
            var initialCount = _screenStack.Count;
            for (var i = 0; i < initialCount; i++)
            {
                var screenSettings = _screenStack.Pop();
                if (screenSettings.IsUnClosable)
                {
                    _screenStack.Push(screenSettings);
                }
                else
                {
                    screenSettings.Screen.Close();
                }
            }
        }

        public static T GetScreen<T>() where T : UIScreen
        {
            var requiredScreen = (T)_screenRegistry[typeof(T)].Screen;

            if (requiredScreen == null)
            {
                Debug.LogError($"You are trying to get not registry screen. " +
                               $"Provide {typeof(T).Name} screen to ScreensRegister");
                
                return default;
            }
            
            return (T)_screenRegistry[typeof(T)].Screen;
        }
        
        public static List<UIScreen> GetAllScreens()
        {
            return _screenRegistry.Select(keyValuePair => keyValuePair.Value.Screen).ToList();
        }

        #endregion

        #region Events Handlers

        private void OnScreenClosed(string guid)
        {
            if(_screenStack.All(screenSettings => screenSettings.Guid != guid))
                return;
            
            if (_screenStack.Count > 0 && _screenStack.Peek().Guid == guid)
            {
                Debug.Log($"{guid} screen closed and removed from the stack.");
                _screenStack.Pop();
                return;
            }
           
            Debug.Log($"Screen of type {guid} closed with stack rebuilding.");
            RebuildStackWithoutGuid(guid);
        }

        private void OnScreenOpened(string guid)
        {
            if (_screenStack.Any(screenSettings => screenSettings.Guid == guid))
                return;

            var screenSettings = _screenRegistry.Values.FirstOrDefault(screen => screen.Guid == guid);
            if (screenSettings == null)
            {
                Debug.LogError($"Cannot open screen with GUID {guid}. Not found in registry.");
                return;
            }

            _screenStack.Push(screenSettings);
            screenSettings.Screen.Open();
            Debug.Log($"Screen {guid} added to the stack and opened.");
        }

        #endregion

        #region Private Helpers

        private static void ExecuteOnScreen<UIScreenT>(Action<UIScreenSettings> action) where UIScreenT : UIScreen
        {
            var screenType = typeof(UIScreenT);
            if (_screenRegistry.TryGetValue(screenType, out var screen))
            {
                action?.Invoke(screen);
            }
            else
            {
                Debug.LogError($"Screen of type {screen.Guid} is not registered.");
            }
        }

        private void RebuildStackWithoutGuid(string guid)
        {
            var tempStack = new Stack<UIScreenSettings>();

            while (_screenStack.Count > 0)
            {
                var screenSettings = _screenStack.Pop();
                if (screenSettings.Guid != guid)
                {
                    tempStack.Push(screenSettings);
                }
            }

            while (tempStack.Count > 0)
            {
                _screenStack.Push(tempStack.Pop());
            }
        }

        #endregion
    }
}