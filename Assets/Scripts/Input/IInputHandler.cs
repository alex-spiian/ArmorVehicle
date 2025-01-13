using System;

namespace ArmorVehicle
{
    public interface IInputHandler
    {
        public event Action Clicked;
        public event Action<float> Rotate;
    }
}