using System;
using UnityEngine;

namespace Weapon.Turret
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField]
        private Turret _turret;

        [SerializeField]
        private TurretRotator _rotator;

        private bool _canAttack;
        private void Update()
        {
            if (!Input.GetMouseButton(0) || !_canAttack) return;
            
            _turret.TryAttack();
            _rotator.Rotate(transform);
        }

        public void Block()
        {
            _canAttack = false;
        }
        
        public void UnBlock()
        {
            _canAttack = true;
        }
    }
}
