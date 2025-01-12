using System;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(HealthController))]
    public class Enemy : MonoBehaviour
    {
        public IHealth Health => _healthController;
        private HealthController _healthController;
        private StateMachine _stateMachine;
        private Action<Enemy> _onEnemyDiedCallBack;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
        }

        public void Initialize(float maxHealth, IHealthHandler target, Action<Enemy> onEnemyDiedCallBack)
        {
            _onEnemyDiedCallBack = onEnemyDiedCallBack;
            _healthController.Initialize(maxHealth);
            CreateStateMachine(target);

            Health.Died += OnDied;
        }

        public void Reset()
        {
            Health.Died -= OnDied;
        }

        public void Kill()
        {
            _healthController.TakeDamage(float.MaxValue);
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

        private void OnDied()
        {
            _onEnemyDiedCallBack?.Invoke(this);
        }
    }
}