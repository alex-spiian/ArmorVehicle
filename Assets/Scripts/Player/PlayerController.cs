using System;
using UnityEngine;
using VContainer;

namespace ArmorVehicle
{
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(WeaponController))]
    [RequireComponent(typeof(HealthController))]
    public class PlayerController : MonoBehaviour
    {
        public Action<bool> Died;
        public IHealthHandler HealthHandler => _healthController;
        public Transform CameraTarget => transform;
        
        private WeaponController _weaponController;
        private MovementController _movementController;
        private HealthController _healthController;
        private IInputHandler _inputHandler;
        private HealthBarManager _healthBarManager;
        private Level _currentLevel;

        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
            _weaponController = GetComponent<WeaponController>();
            _healthController = GetComponent<HealthController>();
            _healthController.Died += OnDied;
        }

        private void OnDestroy()
        {
            _healthController.Died -= OnDied;
        }

        [Inject]
        public void Construct(IInputHandler inputHandler, HealthBarManager healthBarManager)
        {
            _healthBarManager = healthBarManager;
            _inputHandler = inputHandler;
        }

        public void Initialize(Level level)
        {
            _currentLevel = level;
            transform.position = _currentLevel.StartPoint.position;
            _weaponController.Initialize(_inputHandler);
            _healthController.Initialize(100);
            _healthBarManager.Spawn(_healthController, HealthBarType.Player);
        }

        public void StartMoving(Action<bool> levelFinishedCallBack)
        {
            _movementController.Initialize(_currentLevel.SplineContainer, _currentLevel.EndPoint.position, levelFinishedCallBack);
        }

        public void Restart()
        {
            _healthBarManager.Remove(_healthController, HealthBarType.Player);
            _movementController.Stop();
        }

        private void OnDied()
        {
            Restart();
            Died?.Invoke(false);
        }
    }
}