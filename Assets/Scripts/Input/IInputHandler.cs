using System;

namespace ArmorVehicle
{
    public interface IInputHandler
    {
        public event Action MouseButtonDown;
        public event Action<float> Rotate;
    }
}