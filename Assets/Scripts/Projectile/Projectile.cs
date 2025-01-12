using System;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(TrailRenderer))]
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> LifeTimeWasOver;

        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed ;
        
        private TrailRenderer _trailRenderer;
        private Vector3 _directionNormalized;
        private Vector3 _previousPosition;
        private float _damage;
        private float _currentTime;
        private bool _isActive;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            if (!_isActive)
                return;

            Move();
            HandleLifeTime();
            TryAttack();
        }

        public void Initialize(Vector3 spawnPosition, Vector3 direction, float damage)
        {
            transform.position = spawnPosition;
            _directionNormalized = direction.normalized;
            _damage = damage;
            _isActive = true;
            _trailRenderer.Clear();
            _currentTime = 0f;

            _previousPosition = transform.position;
        }

        private void Move()
        {
            transform.position += _directionNormalized * (_speed * Time.deltaTime);
        }
        
        private void TryAttack()
        {
            if (Physics.Raycast(_previousPosition, _directionNormalized, out RaycastHit hit,
                    Vector3.Distance(_previousPosition, transform.position)))
            {
                HandleAttack(hit);
            }
            _previousPosition = transform.position;
        }

        private void HandleAttack(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<IHealthHandler>(out var healthHandler))
            {
                Attack(healthHandler);
                LifeTimeWasOver?.Invoke(this);
                Disable();
            }
        }

        private void Attack(IHealthHandler component)
        {
            component.TakeDamage(_damage);
        }

        private void HandleLifeTime()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _lifeTime)
            {
                Disable();
                LifeTimeWasOver?.Invoke(this);
            }
        }

        private void Disable()
        {
            _isActive = false;
            _currentTime = 0f;
        }
    }
}