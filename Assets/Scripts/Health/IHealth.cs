using System;

namespace Health
{
    public interface IHealth
    {
        event Action Died;
        event Action<float> HealthChanged;
        
        bool IsDead { get; }
        float Health { get; }
        float MaxHealth { get; }
    }
}