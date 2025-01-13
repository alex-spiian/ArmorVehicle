using System;
using UnityEngine;
using VContainer.Unity;

namespace ArmorVehicle
{
    public class MobileInputHandler : ITickable, IInputHandler
    {
        public event Action Clicked;
        public event Action<float> Rotate;

        public void Tick()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Clicked?.Invoke();
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    Rotate?.Invoke(touch.deltaPosition.x);
                }
            }
        }
    }
}