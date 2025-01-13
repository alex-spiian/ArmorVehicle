using UnityEngine;

namespace ArmorVehicle
{
    public class AttackState : MonoBehaviour, IPayLoadedState<EnemyAiData>
    {
        private float _damage;
        private float _attackDistance;
        private float _attackCooldown;
        
        private IHealthHandler _target;
        private IHealthHandler _healthHandler;
        private StateMachine _stateMachine;
        private bool _isActive;
        private float _currentCooldownTime;
        private EnemyAiData _enemyAiData;

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

        public void OnEnter(EnemyAiData enemyAiData)
        {
            _enemyAiData = enemyAiData;
            _target = _enemyAiData.Target;
            _damage = _enemyAiData.EnemyConfig.Damage;
            _attackDistance = _enemyAiData.EnemyConfig.AttackDistance;
            _attackCooldown = _enemyAiData.EnemyConfig.AttackCooldown;
            
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
            _stateMachine.Enter<FollowingTargetState, EnemyAiData>(_enemyAiData);
        }
    }
}