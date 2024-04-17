using System;
using Death;
using DefaultNamespace;
using UnityEngine;

namespace Health
{
    public class HealthController : MonoBehaviour, IHealthHandler
    {
        public event Action Died;
        public event Action Damaged;
        public event Action<float> HealthChanged;
        
        [field:SerializeField]
        public float MaxHealth { get; private set; }
        public bool IsDead => Health <= 0;
        public float Health { get; private set; }

        public void Initialize()
        {
            Health = MaxHealth;
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

            if (IsDead)
            {
                Died?.Invoke();
            }
        }
    }
}