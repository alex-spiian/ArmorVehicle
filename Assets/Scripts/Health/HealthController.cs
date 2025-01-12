using System;
using UnityEngine;

namespace ArmorVehicle
{
    public class HealthController : MonoBehaviour, IHealthHandler
    {
        public event Action Died;
        public event Action Damaged;
        public event Action<float> HealthChanged;

        public bool IsDead => Health <= 0;
        public Transform Owner => transform;
        public float Health { get; private set; }

        private float _maxHealth;

        public void Initialize(float maxHealth)
        {
            _maxHealth = maxHealth;
            Health = _maxHealth;
            HealthChanged?.Invoke(Health);
        }
        
        public void TakeDamage(float damage)
        {
            if (damage >= Health)
            {
                Health = 0;
            }
            else
            {
                Health -= damage;
            }

            HealthChanged?.Invoke(Health);
            Damaged?.Invoke();

            if (IsDead)
            {
                Died?.Invoke();
            }
        }
    }
}