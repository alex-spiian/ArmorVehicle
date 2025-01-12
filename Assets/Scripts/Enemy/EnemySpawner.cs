using System.Collections.Generic;
using UnityEngine;

namespace ArmorVehicle
{
    public class EnemySpawner
    {
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private readonly List<Enemy> _enemies = new();
        private readonly HealthBarManager _healthBarManager;
        private readonly IHealthHandler _target;
        private readonly MonoBehaviourPool<Enemy> _enemyPool;

        public EnemySpawner(EnemySpawnerConfig enemySpawnerConfig, HealthBarManager healthBarManager, IHealthHandler target)
        {
            _target = target;
            _healthBarManager = healthBarManager;
            _enemySpawnerConfig = enemySpawnerConfig;
            _enemyPool = new MonoBehaviourPool<Enemy>(_enemySpawnerConfig.EnemyPrefab, null);
        }
        
        public void Spawn(Level level, int currentLevelNumber)
        {
            RemovePrevious();
            
            var multiplierByLevel = 1 + (currentLevelNumber - 1) * _enemySpawnerConfig.EnemyIncreasePercentPerLevel;

            for (var zoneNumber = 0; zoneNumber < level.Zones.Length; zoneNumber++)
            {
                var multiplierByZone = 1 + (zoneNumber - 1) * _enemySpawnerConfig.EnemyIncreasePercentPerZone;
                var enemyCount = Mathf.FloorToInt(_enemySpawnerConfig.MinEnemiesOnZone * (multiplierByLevel + multiplierByZone));
                var currentZone = level.Zones[zoneNumber];
                
                if (!currentZone.ShouldSpawnEnemies)
                {
                    continue;
                }

                for (var j = 0; j < enemyCount; j++)
                {
                    var spawnPosition = GetRandomPositionInZone(currentZone);
                    var enemy = _enemyPool.Take();
                    enemy.transform.position = spawnPosition;
                    enemy.Initialize(20, _target, OnEnemyDied);
                    _enemies.Add(enemy);
                    _healthBarManager.Spawn(enemy.Health, HealthBarType.Enemy);
                }
            }
        }

        private void RemovePrevious()
        {
            foreach (var enemy in _enemies)
            {
                _healthBarManager.Remove(enemy.Health, HealthBarType.Enemy);
                _enemyPool.Release(enemy);
            }
            
            _enemies.Clear();
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.Reset();
            _healthBarManager.Remove(enemy.Health, HealthBarType.Enemy);
            _enemies.Remove(enemy);
            _enemyPool.Release(enemy);
        }

        private Vector3 GetRandomPositionInZone(LevelZone zone)
        {
            var randomX = Random.Range(-zone.Size.x / 2f, zone.Size.x / 2f);
            var randomZ = Random.Range(-zone.Size.y / 2f, zone.Size.y / 2f);
            return zone.CenterPoint.position + new Vector3(randomX, 0f, randomZ);
        }
    }
}