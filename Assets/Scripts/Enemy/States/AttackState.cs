using Health;
using UnityEngine;

namespace Enemy.States
{
    public class AttackState : MonoBehaviour, IPayLoadedState<IHealthHandler>
    {
        [SerializeField]
        private float _damage = 5;
        private IHealthHandler _healthHandler;
        
        public void Initialize(StateMachine stateMachine)
        {
            _healthHandler = GetComponent<IHealthHandler>();
        }

        public void OnEnter(IHealthHandler healthHandler)
        {
            Attack(healthHandler);
        }

        private void Attack(IHealthHandler healthHandler)
        {
            healthHandler.TakeDamage(_damage);
            _healthHandler.TakeDamage(100);
        }
    }
}