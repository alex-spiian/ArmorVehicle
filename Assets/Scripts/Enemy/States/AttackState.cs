using Health;
using UnityEngine;

namespace Enemy.States
{
    public class AttackState : MonoBehaviour, IPayLoadedState<IHealthHandler>
    {
        private StateMachine _stateMachine;
        private IHealthHandler _healthHandler;
        private float _damage = 5;
        public void Initialize(StateMachine stateMachine)
        {
            _healthHandler = GetComponent<IHealthHandler>();
            _stateMachine = stateMachine;
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