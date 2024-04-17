using System;
using Health;
using UnityEngine;

namespace Car
{
    public class Car : MonoBehaviour
    {
        public event Action Lost; 
        public event Action Won;
        
        [field:SerializeField]
        public HealthController HealthController { get; private set; }
        
        [SerializeField]
        private MovementController _movementController;
        
        private HealthBarPositionController _healthBar;
        private HealthBarFactory _healthBarFactory;
        private void Awake()
        {
            HealthController.Died += OnLost;
            _movementController.CameToFinish += OnWon;
        }

        public void OnLevelStarted(Vector3 finishPosition)
        {
            HealthController.Reset();
            _movementController.SetDestination(finishPosition);
            _healthBar.gameObject.SetActive(true);
        }

        public void Initialize(HealthBarPositionController healthBar, RectTransform canvas)
        {
            if (_healthBar != null) return;
            
            _healthBarFactory = new HealthBarFactory();
            _healthBar = _healthBarFactory.Create(healthBar, canvas, transform);
            _healthBar.gameObject.SetActive(false);
        }
        private void OnLost()
        {
            _movementController.Stop();
            Lost?.Invoke();
        }
        
        private void OnWon()
        {
            _movementController.Stop();
            _healthBar.gameObject.SetActive(false);
            Won?.Invoke();
        }
    }
}