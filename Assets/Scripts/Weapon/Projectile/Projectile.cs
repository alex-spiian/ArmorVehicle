using System;
using Health;
using UnityEngine;

namespace Weapon.Projectile
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> LifeTimeWasOver;
        [SerializeField]
        private TrailRenderer _trailRenderer;
        
        [SerializeField]
        private int _lifeTime = 5;

        [SerializeField]
        private int _speedForce = 150;
        
        private float _damage;
        private float _currentTime;
        private Rigidbody _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Attack(Vector3 direction)
        {
            _trailRenderer.Clear();

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction * _speedForce, ForceMode.VelocityChange);
        }
        
        private void Update()
        {
            HandleLifeTime();
        }

        public void Initialize(Vector3 spawnPosition, Vector3 direction, float damage)
        {
            transform.position = spawnPosition;
            direction = direction.normalized;
            _damage = damage;
            Attack(direction);
        }
        private void OnTriggerEnter(Collider otherCollider)
        {
            otherCollider.gameObject.TryGetComponent<IHealthHandler>(out var collider);
            
            if (collider == null) return;

            LifeTimeWasOver?.Invoke(this);
            Hit(collider, _damage);
        }

        private void Hit(IHealthHandler component, float damage)
        {
            component.TakeDamage(damage);
        }

        private void HandleLifeTime()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _lifeTime)
            {
                LifeTimeWasOver?.Invoke(this);

                _currentTime = 0;
            }
        }
    }
}