using Health;
using UnityEngine;

namespace Enemy.States
{
    public class FollowingTargetState : MonoBehaviour, IPayLoadedState<Transform>
    {

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speed = 40;
        
        private bool _isActive;
        private Transform _target;
        private StateMachine _stateMachine;
        private IHealth _health;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _health.Died += Stop;
        }

        private void Update()
        {
            if (!_isActive)
            {
                _animator.SetBool(IsMoving, false);
                return;
            }

            _animator.SetBool(IsMoving, true);
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.LookAt(_target);
            _rigidbody.MovePosition(transform.position + direction * _speed * Time.deltaTime);

            if (_health.IsDead)
            {
                _isActive = false;
            }
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter(Transform target)
        {
            _target = target;
            _isActive = true;
        }

        private void EnterAttackState(IHealthHandler healthHandler)
        {
            _stateMachine.Enter<AttackState, IHealthHandler>(healthHandler);
        }

        private void Stop()
        {
            _animator.SetBool(IsMoving, false);
            _isActive = false;
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            otherCollider.gameObject.TryGetComponent<IHealthHandler>(out var collider);
            if (collider == null) return;

            Stop();
            EnterAttackState(collider);
        }

        private void OnDestroy()
        {
            _health.Died -= Stop;
        }
    }
}