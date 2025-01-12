using UnityEngine;

namespace ArmorVehicle
{
    public class IdleState : MonoBehaviour, IPayLoadedState<IHealthHandler>
    {
        [SerializeField] private float _targetDetectionDistance;
        
        private StateMachine _stateMachine;
        private IHealthHandler _target;
        private bool _isActive;

        public void OnEnter(IHealthHandler target)
        {
            _target = target;
            _isActive = true;
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Update()
        {
            if (!_isActive)
                return;
            

            if (_target != null && Vector3.Distance(transform.position, _target.Owner.position) <= _targetDetectionDistance)
            {
                EnterFollowingTargetState();
            }
        }

        private void EnterFollowingTargetState()
        {
            _isActive = false;
            _stateMachine.Enter<FollowingTargetState, IHealthHandler>(_target);
        }
    }
}