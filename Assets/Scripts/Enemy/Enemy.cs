using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(HealthController))]
    public class Enemy : MonoBehaviour
    {
        public IHealth Health => _healthController;
        private HealthController _healthController;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
        }

        public void Initialize(float maxHealth, IHealthHandler target)
        {
            _healthController.Initialize(maxHealth);
            CreateStateMachine(target);
        }

        private void CreateStateMachine(IHealthHandler target)
        {
            if (_stateMachine == null)
            {
                _stateMachine = new StateMachine
                (
                    GetComponent<IdleState>(),
                    GetComponent<FollowingTargetState>(),
                    GetComponent<AttackState>()
                );
            
                _stateMachine.Initialize();
            }
            
            _stateMachine.Enter<IdleState, IHealthHandler>(target);
        }
    }
}