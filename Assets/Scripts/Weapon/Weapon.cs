using Cysharp.Threading.Tasks;
using Enemy;
using UnityEngine;

namespace Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _damage;
        [SerializeField] private float _attackCooldown;

        private float _currentCooldownTime;

        public void TryAttack()
        {
            if (_currentCooldownTime < _attackCooldown)
            {
                return;
            }

            _currentCooldownTime = 0;

            Attack();
        }
        protected virtual void Update()
        {
            _currentCooldownTime += Time.deltaTime;
        }
        
        protected abstract void Attack();

    }
}