using UnityEngine;

namespace ArmorVehicle
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _damage;
        [SerializeField] private float _attackCooldown;

        protected IInputHandler InputHandler;
        protected bool CanAttack;
        private float _currentCooldownTime;

        public virtual void Initialize(IInputHandler inputHandler)
        {
            InputHandler = inputHandler;
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