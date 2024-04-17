using System;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class Road : MonoBehaviour
    {
        public event Action RoadPassed;
        
        [field:SerializeField]
        public RoadSizeProvider RoadSizeProvider { get; private set; }

        private readonly List<Enemy.Enemy> _enemies = new();
        private Transform _target;

        private void OnTriggerEnter(Collider other)
        {
            _target = other.transform;
            if (other.CompareTag("Car"))
            {
                AgrEnemy();
                RoadPassed?.Invoke();
            }
        }

        public void AddEnemy(Enemy.Enemy enemy)
        {
            _enemies.Add(enemy);

            enemy.Dead += OnEnemyDied;
        }

        public void Reset()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                _enemies[i].Reset();
            }
            _enemies.Clear();
        }
        
        private void OnEnemyDied(Enemy.Enemy enemy)
        {
            _enemies.Remove(enemy);
        }

        private void AgrEnemy()
        {
            foreach (var enemy in _enemies)
            {
                enemy.EnterFollowingTargetState(_target);
            }
        }
    }
}
