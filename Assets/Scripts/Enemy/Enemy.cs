using System;
using Enemy.States;
using Health;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Dead;

        [field:SerializeField]
        public HealthController HealthController { get; private set; }
        
        [SerializeField]
        private int _critDamage;
        
        private StateMachine _stateMachine;
        private HealthBarPositionController _healthBar;
        private HealthBarFactory _healthBarFactory;

        private void Start()
        {
            HealthController.Died += OnDead;
            HealthController.Initialize();

            _stateMachine = new StateMachine
            (
                GetComponent<FollowingTargetState>(),
                GetComponent<AttackState>()
            );
            
            _stateMachine.Initialize();
        }

        public void Initialize(HealthBarPositionController healthBar, RectTransform canvas)
        {
            if (_healthBar != null) return;
            
            _healthBarFactory = new HealthBarFactory();
            _healthBar = _healthBarFactory.Create(healthBar, canvas, transform);
            _healthBar.gameObject.SetActive(false);
        }
        public void EnterFollowingTargetState(Transform target)
        {
            _stateMachine.Enter<FollowingTargetState, Transform>(target);
        }
        public void Reset()
        {
            HealthController.TakeDamage(_critDamage);
            Dead?.Invoke(this);
        }

        private void OnDead()
        {
            Dead?.Invoke(this);
        }

        private void OnDestroy()
        {
            HealthController.Died -= OnDead;
        }

    }
}