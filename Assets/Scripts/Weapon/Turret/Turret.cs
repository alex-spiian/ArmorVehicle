using UnityEngine;

namespace ArmorVehicle
{
    public class Turret : Weapon
    {
        [SerializeField] private TurretRotator _rotator;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileAnchor;
        
        private MonoBehaviourPool<Projectile> _projectilePool;

        public override void Initialize(IInputHandler inputHandler)
        {
            base.Initialize(inputHandler);
            
            InputHandler.MouseButtonDown += TryAttack;
            InputHandler.Rotate += _rotator.Rotate;
            CanAttack = true;
        }
        
        private void Awake()
        {
            _projectilePool = new MonoBehaviourPool<Projectile>(_projectilePrefab, null);
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
            var projectile = _projectilePool.Take();
            projectile.LifeTimeWasOver += OnProjectileLifeTimeWasOver;
            projectile.Initialize(_projectileAnchor.position, -transform.up, _damage);
        }

        private void OnProjectileLifeTimeWasOver(Projectile projectile)
        {
            projectile.LifeTimeWasOver -= OnProjectileLifeTimeWasOver;
            _projectilePool.Release(projectile);
        }
    }
}