using System;
using Death;
using DefaultNamespace;
using Enemy.States;
using Health;
using Order;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IMortal
    {
        public event Action<Enemy> Dead;

        [field:SerializeField]
        public CharacterType CharacterType { get; private set; }
        [field:SerializeField]
        public HealthController HealthController { get; private set; }

        [SerializeField] private int _critDamage;
        
        private StateMachine _stateMachine;
        private HealthBarPositionController _healthBar;
        private HealthBarFactory _healthBarFactory;

        private void Start()
        {
            HealthController.Died += OnDead;
            HealthController.Initialize();

            _stateMachine = new StateMachine
            (
                GetComponent<IdleState>(),
                GetComponent<FollowingTargetState>(),
                GetComponent<AttackState>()
            );
            
            _stateMachine.Initialize();
            _stateMachine.Enter<IdleState>();
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