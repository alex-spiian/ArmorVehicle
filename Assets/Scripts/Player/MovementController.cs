using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace ArmorVehicle.Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
    
        private SplineContainer _splineContainer;
        private float _distanceAlongSpline;
        private float _splineLength;
        private bool _canMove;

        public void Initialize(SplineContainer splineContainer)
        {
            _splineContainer = splineContainer;
            _canMove = true;
        }

        public void Stop()
        {
            _canMove = false;
        }
    
        private void Update()
        {
            if (!_canMove)
                return;
        
            _distanceAlongSpline += _speed * Time.deltaTime;
            if (_distanceAlongSpline > _splineLength)
            {
                _distanceAlongSpline = 0f;
            }
        
            var normalizedDistance = _distanceAlongSpline / _splineLength;
            var position = _splineContainer.EvaluatePosition(normalizedDistance);
            var tangent = _splineContainer.EvaluateTangent(normalizedDistance);
            transform.position = new Vector3(position.x, position.y, position.z);
        
            HandleRotation(tangent);
        }

        private void HandleRotation(float3 tangent)
        {
            if (tangent.x != 0f || tangent.z != 0f)
            {
                var targetRotation = Quaternion.LookRotation(
                    new Vector3(tangent.x, 0f, tangent.z)
                );
                transform.rotation = targetRotation;
            }
        }
    }
}