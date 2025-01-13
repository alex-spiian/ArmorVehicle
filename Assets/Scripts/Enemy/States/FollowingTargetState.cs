using UnityEngine;

namespace ArmorVehicle
{
    public class FollowingTargetState : MonoBehaviour, IPayLoadedState<IHealthHandler>
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speed;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _targetDetectionDistance;

        private bool _isActive;
        private IHealthHandler _target;
        private StateMachine _stateMachine;
        private IHealth _health;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _health.Died += OnDied;
        }
        
        private void OnDestroy()
        {
            _health.Died -= OnDied;
        }

        private void Update()
        {
            if (!_isActive)
            {
                _animator.SetBool(IsMoving, false);
                return;
            }
            
            if (_health.IsDead)
            {
                _isActive = false;
            }

            if (Vector3.Distance(transform.position, _target.Owner.position) <= _attackDistance)
            {
                EnterAttackState();
            }
            
            if (Vector3.Distance(transform.position, _target.Owner.position) > _targetDetectionDistance)
            {
                EnterIdleState();
            }

            Move();
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

        private void Move()
        {
            _animator.SetBool(IsMoving, true);
            var direction = (_target.Owner.position - transform.position).normalized;
            transform.LookAt(_target.Owner);
            _characterController.Move(direction * _speed * Time.deltaTime);
        }

        private void EnterAttackState()
        {
            _isActive = false;
            _stateMachine.Enter<AttackState, IHealthHandler>(_target);
        }
        
        private void EnterIdleState()
        {
            _isActive = false;
            _stateMachine.Enter<IdleState, IHealthHandler>(_target);
        }

        private void OnDied()
        {
            _animator.SetBool(IsMoving, false);
            _isActive = false;
        }
    }
}