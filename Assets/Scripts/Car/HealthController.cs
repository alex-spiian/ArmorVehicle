using System;
using Death;
using DefaultNamespace;
using Health;
using UnityEngine;

namespace Car
{
    public class HealthController : MonoBehaviour, IHealthHandler, IMortal
    {
        public event Action Died;
        public event Action Damaged;
        public event Action<float> HealthChanged;
        [field:SerializeField] public CharacterType CharacterType { get; private set; }

        private DeathHandler _deathHandler = new(); 
        
        [field:SerializeField]
        public float MaxHealth { get; private set; }
        public bool IsDead => Health <= 0;
        public float Health { get; private set; }

        public void Awake()
        {
            Health = MaxHealth;
            Died += _deathHandler.Test;
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

        public void Reset()
        {
            Health = MaxHealth;
            HealthChanged?.Invoke(Health);
        }

    }
}