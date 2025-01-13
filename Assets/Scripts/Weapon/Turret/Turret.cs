using System;
using DG.Tweening;
using UnityEngine;

namespace ArmorVehicle
{
    public class Turret : Weapon
    {
        [SerializeField] private TurretRotator _rotator;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileAnchor;
        [SerializeField] private Vector3 _knockBackOffset;
        [SerializeField] private float _knockBackDuration;

        private MonoBehaviourPool<Projectile> _projectilePool;
        private Vector3 _initialPosition;
        private Vector3 _knockBackTarget;

        public override void Initialize(IInputHandler inputHandler, float damage, float attackCooldown)
        {
            base.Initialize(inputHandler, damage, attackCooldown);
            
            InputHandler.MouseButtonDown += TryAttack;
            InputHandler.Rotate += _rotator.Rotate;
            CanAttack = true;
        }
        
        private void Awake()
        {
            _projectilePool = new MonoBehaviourPool<Projectile>(_projectilePrefab, null);
            _initialPosition = transform.localPosition;
        }
        
        private void OnDestroy()
        {
            InputHandler.MouseButtonDown -= TryAttack;
            InputHandler.Rotate -= _rotator.Rotate;
        }

        protected override void Attack()
        {
            ApplyKnockBack(CreateProjectile);
        }

        private void CreateProjectile()
        {
            var projectile = _projectilePool.Take();
            projectile.LifeTimeWasOver += OnProjectileLifeTimeWasOver;
            projectile.Initialize(_projectileAnchor.position, transform.forward, _damage);
        }

        private void ApplyKnockBack(Action onComplete)
        {
            var knockBackDirection = transform.TransformDirection(-Vector3.forward) * _knockBackOffset.magnitude;
            _knockBackTarget = _initialPosition + knockBackDirection;

            transform.DOLocalMove(_knockBackTarget, _knockBackDuration / 2)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                    transform.DOLocalMove(_initialPosition, _knockBackDuration / 2);
                });
        }

        private void OnProjectileLifeTimeWasOver(Projectile projectile)
        {
            projectile.LifeTimeWasOver -= OnProjectileLifeTimeWasOver;
            _projectilePool.Release(projectile);
        }
    }
}