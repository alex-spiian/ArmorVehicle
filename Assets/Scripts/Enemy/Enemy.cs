using System;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(HealthController))]
    [RequireComponent(typeof(FxHandler))]
    public class Enemy : MonoBehaviour
    {

        public IHealth Health => _healthController;
        private Action<Enemy> _onEnemyDiedCallBack;
        private HealthController _healthController;
        private StateMachine _stateMachine;
        private FxHandler _fxHandler;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _fxHandler = GetComponent<FxHandler>();
        }

        public void Initialize(EnemyConfig enemyConfig, IHealthHandler target, Action<Enemy> onEnemyDiedCallBack)
        {
            _onEnemyDiedCallBack = onEnemyDiedCallBack;
            _healthController.Initialize(enemyConfig.MaxHealth);
            CreateStateMachine(enemyConfig, target);

            Health.Died += OnDied;
            _fxHandler.Reset();
        }

        public void Reset()
        {
            Health.Died -= OnDied;
        }

        public void Kill()
        {
            _healthController.TakeDamage(float.MaxValue);
        }

        private void CreateStateMachine(EnemyConfig enemyConfig, IHealthHandler target)
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

            var enemyAiData = new EnemyAiData(enemyConfig, target);
            
            _stateMachine.Enter<IdleState, EnemyAiData>(enemyAiData);
        }

        private void OnDied()
        {
            _onEnemyDiedCallBack?.Invoke(this);
        }
    }
}