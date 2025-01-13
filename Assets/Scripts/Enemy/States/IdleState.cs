using UnityEngine;

namespace ArmorVehicle
{
    public class IdleState : MonoBehaviour, IPayLoadedState<EnemyAiData>
    {
        private float _targetDetectionDistance;
        private StateMachine _stateMachine;
        private IHealthHandler _target;
        private bool _isActive;
        private EnemyAiData _enemyAiData;

        public void OnEnter(EnemyAiData enemyAiData)
        {
            _enemyAiData = enemyAiData;
            _target = _enemyAiData.Target;
            _targetDetectionDistance = _enemyAiData.EnemyConfig.TargetDetectionDistance;
            
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
            _stateMachine.Enter<FollowingTargetState, EnemyAiData>(_enemyAiData);
        }
    }
}