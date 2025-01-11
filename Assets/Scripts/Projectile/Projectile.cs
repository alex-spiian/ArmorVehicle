using System;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TrailRenderer))]
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> LifeTimeWasOver;
        [SerializeField] private int _lifeTime;
        [SerializeField] private int _speedForce;
        
        private float _damage;
        private float _currentTime;
        private Rigidbody _rigidbody;
        private TrailRenderer _trailRenderer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _trailRenderer = GetComponent<TrailRenderer>();
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
            otherCollider.gameObject.TryGetComponent<IHealthHandler>(out var healthHandler);
            
            if (healthHandler == null)
                return;

            LifeTimeWasOver?.Invoke(this);
            Hit(healthHandler, _damage);
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