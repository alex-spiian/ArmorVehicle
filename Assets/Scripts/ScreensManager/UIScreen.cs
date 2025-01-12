using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ScreensManager
{
    public abstract class UIScreen : MonoBehaviour
    {
        public event Action<string> Closed;
        public event Action<string> Opened;
        protected UniTaskCompletionSource<OpenLevelType> TaskCompletionSource;

        private string _guid;

        public void Initialize(string guid)
        {
            _guid = guid;
        }

        public virtual void Tick<T>(T screenContext, UniTaskCompletionSource<OpenLevelType> taskCompletionSource)
        {
            TaskCompletionSource = taskCompletionSource;
        }
        
        public virtual void Tick<T>(T screenContext)
        {
            
        }
        
        public virtual void Open()
        {
            Opened?.Invoke(_guid);
            gameObject.SetActive(true);
        }
        
        public virtual void Close()
        {
            Closed?.Invoke(_guid);
            gameObject.SetActive(false);
        }
    }
}