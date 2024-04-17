using System;
using System.Collections.Generic;
using Health;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private HealthBarPositionController _healthBarPrefab;
        [SerializeField] private RectTransform _canvasForHealthBar;
        [SerializeField] private int _minEnemyCount;
        [SerializeField] private int _maxEnemyCount;
        
        private ObjectPool<Enemy> _enemyPool;

        public void Initialize()
        {
            _enemyPool = new ObjectPool<Enemy>(
                createFunc: () => Instantiate(_enemyPrefab),
                actionOnGet: t => t.gameObject.SetActive(true),
                actionOnRelease: t => t.gameObject.SetActive(false));
        }

        public void Spawn(Road.Road road)
        {
            var randomCount = Random.Range(_minEnemyCount, _maxEnemyCount);
            var positions = GetRandomPositions(road, randomCount);
            
            for (int i = 0; i < randomCount; i++)
            {
                var enemy = _enemyPool.Get();

                enemy.transform.position = positions[i];
                enemy.Dead += Release;
                enemy.HealthController.Initialize();
                enemy.Initialize(_healthBarPrefab, _canvasForHealthBar);
                road.AddEnemy(enemy);
            }
            
            positions.Clear();
        }

        private List<Vector3> GetRandomPositions(Road.Road road, int count)
        {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < count; i++)
            {
                var roadSizeProvider = road.RoadSizeProvider;
                var randomX = Random.Range(roadSizeProvider.LeftUpper.position.x, roadSizeProvider.RightUpper.position.x);
                var randomZ = Random.Range(roadSizeProvider.RightUpper.position.z, roadSizeProvider.RightLower.position.z);
                positions.Add(new Vector3(randomX, 0f, randomZ));
            }

            return positions;
        }

        private void Release(Enemy enemy)
        {
            enemy.Dead -= Release;
            _enemyPool.Release(enemy);
        }
    }
}