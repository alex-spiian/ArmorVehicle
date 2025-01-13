using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace ArmorVehicle
{
    public class MovementController : MonoBehaviour
    {
        private Action _levelFinishedCallBack;
        private SplineContainer _splineContainer;
        private Vector3 _finishPosition;
        private float _speed;
        private float _distanceAlongSpline;
        private float _splineLength;
        private bool _canMove;

        public void Initialize(float speed, SplineContainer splineContainer, Vector3 finishPosition, Action levelFinishedCallBack)
        {
            _levelFinishedCallBack = levelFinishedCallBack;
            _finishPosition = finishPosition;
            _splineContainer = splineContainer;
            _speed = speed;
            _splineLength = _splineContainer.CalculateLength();

            Reset();
            UpdatePosition();
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
            HandleDistanceToFinish();
        }

        private void Reset()
        {
            _distanceAlongSpline = 0f;
            _canMove = true;
        }

        private void UpdatePosition()
        {
            var initialPosition = _splineContainer.EvaluatePosition(0f);
            transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
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

        private void HandleDistanceToFinish()
        {
            const float threshold = 0.5f;
            if (Vector3.Distance(transform.position, _finishPosition) <= threshold)
            {
                Stop();
                _levelFinishedCallBack?.Invoke();
            }
        }
    }
}