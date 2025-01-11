using System;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(MovementController))]
    public class PlayerController : MonoBehaviour
    {
         private MovementController _movementController;

         private void Awake()
         {
             _movementController = GetComponent<MovementController>();
         }

         public void Initialize(Level level, Action<bool> levelFinishedCallBack)
        {
            _movementController.Initialize(level.SplineContainer, level.EndPoint.position, levelFinishedCallBack);
        }

        public void Disable()
        {
            _movementController.Stop();
        }
    }
}