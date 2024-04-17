using System;
using UnityEngine;

namespace Weapon.Turret
{
    [Serializable]
    public class TurretRotator
    {
        [SerializeField]
        private float _speed;

        public TurretRotator(float speed)
        {
            _speed = speed;
        }
        
        public void Rotate(Transform turret)
        {
            var mouseX = Input.GetAxis("Mouse X");
            Vector3 rotation = new Vector3(0f, 0f, mouseX);
            turret.Rotate(rotation * _speed);
        }
    }
}