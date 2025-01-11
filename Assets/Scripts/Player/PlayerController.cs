using System;
using UnityEngine;

namespace ArmorVehicle.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        
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