using System;
using Cysharp.Threading.Tasks;
using Health;
using Scripts;
using UnityEngine;

namespace Car
{
    public class CarVisualEffector : VisualEffector
    {
        [SerializeField] protected float _speedForce;
        [SerializeField] private Collider _collider;
        private Rigidbody _rigidbody;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _healthHandler = GetComponent<IHealthHandler>();
            _healthHandler.Died += OnDied;
            _healthHandler.Damaged += OnDamaged;
        }

        protected override async UniTask ShowDeath()
        {
            _rigidbody.isKinematic = false;
            _collider.isTrigger = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _speedForce, ForceMode.VelocityChange);
           _effect.SetActive(true);
        }

        protected override async UniTask ShowDamage()
        {
            // _material.color = Color.white;
            // await UniTask.Delay(100);
            // _material.color = _initialColor;
        }
    }
}