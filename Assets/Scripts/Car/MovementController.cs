using System;
using UnityEngine;

namespace Car
{
   public class MovementController : MonoBehaviour
   {
      public event Action CameToFinish;
      public event Action StartedMoving;
      
      [SerializeField] private Vector3 _finish;
      [SerializeField] private float _speed;
      
      private bool _isActive;
      private Vector3 _startPosition;

      private void Update()
      {
         if (!_isActive) return;
         Move(_finish);

         if (transform.position == _finish)
         {
            CameToFinish?.Invoke();
         }
      }

      private void Move(Vector3 targetPosition)
      {
         transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
      }

      public float CalculateMovementProgress()
      {
         var totalDistance = Vector3.Distance(_startPosition, _finish);
         var currentDistance = Vector3.Distance(_startPosition, transform.position);
         return Mathf.Clamp01(currentDistance / totalDistance);
      }
      
      public void SetDestination(Vector3 destination)
      {
         StartedMoving?.Invoke();
         _finish = destination;
         _startPosition = transform.position;
         _isActive = true;
      }
      public void Stop()
      {
         _isActive = false;
      }
   }
}
