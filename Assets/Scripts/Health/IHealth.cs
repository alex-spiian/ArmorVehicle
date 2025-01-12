using System;
using UnityEngine;

namespace ArmorVehicle
{
    public interface IHealth
    {
        public event Action Died;
        public event Action Damaged;
        public event Action<float> HealthChanged;

        public bool IsDead { get; }
        public float Health { get; }
        public float MaxHealth { get; }
        public Transform Owner { get; }
    }
}