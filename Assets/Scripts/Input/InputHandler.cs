using System;
using UnityEngine;
using VContainer.Unity;

namespace ArmorVehicle
{
    public class InputHandler : ITickable, IInputHandler
    {
        public event Action MouseButtonDown;
        public event Action<float> Rotate;
        
        public void Tick()
        {
            if (Input.GetMouseButton(0))
            {
                MouseButtonDown?.Invoke();
                Rotate?.Invoke(Input.GetAxis("Mouse X"));
            }
        }
    }
}