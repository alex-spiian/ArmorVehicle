using System;
using UnityEngine;
using VContainer.Unity;

namespace ArmorVehicle
{
    public class StandaloneInputHandler : ITickable, IInputHandler
    {
        public event Action Clicked;
        public event Action<float> Rotate;
        
        public void Tick()
        {
            if (Input.GetMouseButton(0))
            {
                Clicked?.Invoke();
                Rotate?.Invoke(Input.GetAxis("Mouse X"));
            }
        }
    }
}