using System;
using UnityEngine;

namespace ArmorVehicle
{
    public class AttackState : MonoBehaviour, IPayLoadedState<IHealthHandler>
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _attackCooldown;

        private IHealthHandler _target;
        private IHealthHandler _healthHandler;
        private StateMachine _stateMachine;
        private bool _isActive;
        private float _currentCooldownTime;

        private void Awake()
        {
            _healthHandler = GetComponent<IHealthHandler>();
        }

        private void Update()
        {
            _currentCooldownTime += Time.deltaTime;

            if (!_isActive)
                return;

            TryAttack();
            if (_target != null && Vector3.Distance(transform.position, _target.Owner.position) > _attackDistance)
            {
                EnterFollowingTargetState();
            }
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter(IHealthHandler target)
        {
            _target = target;
            _isActive = true;
        }
        
        private void TryAttack()
        {
            if (_currentCooldownTime < _attackCooldown)
            {
                return;
            }
            
            _currentCooldownTime = 0;
            Attack();
        }

        private void Attack()
        {
            _target.TakeDamage(_damage);
            KillItself();
        }

        private void KillItself()
        {
            _healthHandler.TakeDamage(float.MaxValue);
        }

        private void EnterFollowingTargetState()
        {
            _isActive = false;
            _stateMachine.Enter<FollowingTargetState, IHealthHandler>(_target);
        }
    }
}