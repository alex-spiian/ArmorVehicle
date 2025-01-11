using UnityEngine;
using UnityEngine.Pool;

namespace ArmorVehicle
{
    public class Turret : Weapon
    {
        [SerializeField] private TurretRotator _rotator;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileAnchor;
        [SerializeField] private Vector3 _projectileRotation;
        
        private ObjectPool<Projectile> _projectilePool;

        public override void Initialize(IInputHandler inputHandler)
        {
            base.Initialize(inputHandler);
            
            InputHandler.MouseButtonDown += TryAttack;
            InputHandler.Rotate += _rotator.Rotate;
            CanAttack = true;
        }
        
        private void Awake()
        {
            Quaternion rotation = Quaternion.Euler(_projectileRotation);
            _projectilePool = new ObjectPool<Projectile>(
                createFunc: () => Instantiate(_projectilePrefab, _projectileAnchor.position, rotation),
                actionOnGet: t => t.gameObject.SetActive(true),
                actionOnRelease: OnRelease);
        }
        
        private void OnDestroy()
        {
            InputHandler.MouseButtonDown -= TryAttack;
            InputHandler.Rotate -= _rotator.Rotate;
        }
        
        public void Block()
        {
            CanAttack = false;
        }
        
        public void UnBlock()
        {
            CanAttack = true;
        }

        protected override void Attack()
        {
            CreateProjectile();
        }

        private void CreateProjectile()
        {
            var projectile = _projectilePool.Get();
            projectile.LifeTimeWasOver += OnProjectileLifeTimeWasOver;
            projectile.Initialize(_projectileAnchor.position, -transform.up, _damage);
        }

        private void OnProjectileLifeTimeWasOver(Projectile projectile)
        {
            _projectilePool.Release(projectile);
        }

        private void OnRelease(Projectile projectile)
        {
            projectile.LifeTimeWasOver -= OnProjectileLifeTimeWasOver;
            projectile.gameObject.SetActive(false);
        }
    }
}