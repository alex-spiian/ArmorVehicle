using UnityEngine;

namespace ArmorVehicle
{
    public abstract class Weapon : MonoBehaviour
    {
        protected float _damage;
        private float _attackCooldown;
        protected IInputHandler InputHandler;
        protected bool CanAttack;
        private float _currentCooldownTime;

        public virtual void Initialize(IInputHandler inputHandler, float damage, float attackCooldown)
        {
            InputHandler = inputHandler;
            _damage = damage;
            _attackCooldown = attackCooldown;
        }
        
        public void Enable(bool isActive)
        {
            CanAttack = isActive;
        }
        
        protected virtual void Update()
        {
            _currentCooldownTime += Time.deltaTime;
        }
        
        protected void TryAttack()
        {
            if (_currentCooldownTime < _attackCooldown || !CanAttack)
            {
                return;
            }
            
            _currentCooldownTime = 0;
            Attack();
        }

        protected abstract void Attack();
    }
}