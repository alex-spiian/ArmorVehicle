using System;

namespace ArmorVehicle
{
    public interface IHealth
    {
        event Action Died;
        event Action Damaged;
        event Action<float> HealthChanged;
        
        bool IsDead { get; }
        float Health { get; }
        float MaxHealth { get; }
    }
}