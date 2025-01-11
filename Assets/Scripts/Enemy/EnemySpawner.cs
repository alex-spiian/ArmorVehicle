using System.Collections.Generic;
using UnityEngine;

namespace ArmorVehicle
{
    public class EnemySpawner
    {
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private readonly List<Enemy> _enemies = new();

        public EnemySpawner(EnemySpawnerConfig enemySpawnerConfig)
        {
            _enemySpawnerConfig = enemySpawnerConfig;
        }
        
        public void SpawnEnemiesInZone(Level level, int currentLevelNumber)
        {
            RemovePrevious();
            
            var multiplierByLevel = 1 + (currentLevelNumber - 1) * _enemySpawnerConfig.EnemyIncreasePercentPerLevel;

            for (int zoneNumber = 0; zoneNumber < level.Zones.Length; zoneNumber++)
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
                    var enemy = Object.Instantiate(_enemySpawnerConfig.EnemyPrefab, spawnPosition, Quaternion.identity);
                    _enemies.Add(enemy);
                }
            }
        }

        private void RemovePrevious()
        {
            foreach (var enemy in _enemies)
            {
                Object.Destroy(enemy.gameObject);
            }
            
            _enemies.Clear();
        }

        private Vector3 GetRandomPositionInZone(LevelZone zone)
        {
            var randomX = Random.Range(-zone.Size.x / 2f, zone.Size.x / 2f);
            var randomZ = Random.Range(-zone.Size.y / 2f, zone.Size.y / 2f);
            return zone.CenterPoint.position + new Vector3(randomX, 0f, randomZ);
        }
    }
}