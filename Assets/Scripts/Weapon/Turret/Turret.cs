using System;
using Cysharp.Threading.Tasks;
using Enemy;
using UnityEngine;
using UnityEngine.Pool;

namespace Weapon.Turret
{
    public class Turret : Weapon
    {
        //private WeaponEffect _weaponEffect;
        [SerializeField]
        private Projectile.Projectile _projectilePrefab;
        [SerializeField]
        private Transform _projectileAnchor;
        
        [SerializeField]
        private Vector3 _projectileRotation;
        
        private ObjectPool<Projectile.Projectile> _projectilePool;

        private void Awake()
        {
            Quaternion rotation = Quaternion.Euler(_projectileRotation);
            _projectilePool = new ObjectPool<Projectile.Projectile>(
                createFunc: () => Instantiate(_projectilePrefab, _projectileAnchor.position, rotation),
                actionOnGet: t => t.gameObject.SetActive(true),
                actionOnRelease: OnRelease);
        }

        protected override void Attack()
        {
            CreateProjectile();
        }

        private void CreateProjectile()
        {
            //_weaponEffect.Show();
            var projectile = _projectilePool.Get();
            projectile.LifeTimeWasOver += OnProjectileLifeTimeWasOver;
            projectile.Initialize(_projectileAnchor.position, -transform.up, _damage);
        }

        private void OnProjectileLifeTimeWasOver(Projectile.Projectile projectile)
        {
            _projectilePool.Release(projectile);
        }

        private void OnRelease(Projectile.Projectile projectile)
        {
            projectile.LifeTimeWasOver -= OnProjectileLifeTimeWasOver;
            projectile.gameObject.SetActive(false);
        }
    }
}