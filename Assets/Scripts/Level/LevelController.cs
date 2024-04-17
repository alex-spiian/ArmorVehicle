using System;
using Enemy;
using Road;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        public event Action OnRestarted;

        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private RoadBuilder _roadBuilder;
        [SerializeField] private Car.Car _car;

        [SerializeField] private Transform _finish;

        [SerializeField] private Road.Road[] _roads;

        private bool _shouldWaitToContinue;
        private Vector3 _lastFinishPosition;
        private Vector3 _distance;

        private void Start()
        {
            _enemySpawner.Initialize();
            _roadBuilder.Initialize(_roads);
            _lastFinishPosition = _finish.position;
        }

        private void StartLevel()
        {
            _roadBuilder.Prepare();
        }

        private void Update()
        {
            if (!_shouldWaitToContinue) return;

            if (Input.GetMouseButtonDown(0))
            {
                Reset();
            }
        }

        public void WaitToContinue(Action onRestarted)
        {
            OnRestarted = onRestarted;
            _shouldWaitToContinue = true;
            _roadBuilder.Reset();
        }

        public void Reset()
        {
            OnRestarted?.Invoke();
            _car.OnLevelStarted(_lastFinishPosition += (_lastFinishPosition + _lastFinishPosition) * 0.2f);

            _shouldWaitToContinue = false;
            StartLevel();
        }
    }
}