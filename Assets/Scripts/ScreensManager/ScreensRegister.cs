using System;
using System.Collections.Generic;
using ArmorVehicle.Ui;
using UnityEngine;

namespace ArmorVehicle
{
    public class ScreensRegister : MonoBehaviour
    {
        private readonly Dictionary<Type, UIScreenSettings> _screenRegistry = new();
        
        private void Register()
        {
            AddScreen<EmptyScreen>(isFirstScreen: true, isUnClosable: true);
            AddScreen<TutorialScreen>();
            AddScreen<VictoryScreen>();
            AddScreen<DefeatScreen>();
        }

        public Dictionary<Type, UIScreenSettings> GetScreenRegistry()
        {
            if (_screenRegistry.Count > 0)
                return _screenRegistry;
                
            Register();
            return _screenRegistry;
        }

        private void AddScreen<T>(bool isFirstScreen = false, bool isUnClosable = false)  where T : UIScreen
        {
            _screenRegistry.Add(typeof(T), new UIScreenSettings(GetScreenByType<T>(), typeof(T).Name, isFirstScreen, isUnClosable));
        }

        private UIScreen GetScreenByType<T>() where T : UIScreen
        {
            var requiredScreen = GetComponentInChildren<T>(true);
            if (requiredScreen != null)
            {
                return requiredScreen;
            }
            
            Debug.LogError($"Screen of type {typeof(T)} was not found.");
            return default;
        }
    }
}