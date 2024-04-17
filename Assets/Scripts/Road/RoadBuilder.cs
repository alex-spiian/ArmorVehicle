using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Road
{
    public class RoadBuilder : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawner _enemySpawner;
        [SerializeField]
        private Vector3 _shiftForNextRoad;

        private readonly Queue<Road> _roads = new();
        private Vector3 _lastPosition;

        public void Initialize(Road[] roads)
        {
            foreach (var road in roads)
            {
                _roads.Enqueue(road);
                road.transform.position = _lastPosition += _shiftForNextRoad;
                road.RoadPassed += OnRoadPassed;
            }
        }

        public void Reset()
        {
            foreach (var road in _roads)
            {
                road.Reset();
            }
        }

        public void Prepare()
        {
            Road secondLast = null;
            Road last = null;

            foreach (var item in _roads)
            {
                secondLast = last;
                last = item;
            }

            _enemySpawner.Spawn(secondLast);
            _enemySpawner.Spawn(last);
        }
        
        private void OnRoadPassed()
        {
            var road = _roads.Dequeue();
            road.Reset();
            road.transform.position = _lastPosition += _shiftForNextRoad;
            _enemySpawner.Spawn(road);
            _roads.Enqueue(road);
        }
        
    }
}