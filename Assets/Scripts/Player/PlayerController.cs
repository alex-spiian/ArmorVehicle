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
        public Action<bool> LevelFinished;
        public IHealthHandler HealthHandler => _healthController;
        
        [field:SerializeField] public Transform CameraTarget { get; private set; }
        
        private WeaponController _weaponController;
        private MovementController _movementController;
        private HealthController _healthController;
        private IInputHandler _inputHandler;
        private HealthBarManager _healthBarManager;
        private Level _currentLevel;
        private Action<bool> _levelFinishedCallBack;
        private PlayerConfig _playerConfig;

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

        public void Initialize(Level level, PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
            _currentLevel = level;
            transform.position = _currentLevel.StartPoint.position;
            _weaponController.Initialize(_inputHandler, _playerConfig.Damage, _playerConfig.AttackCooldown);
            _healthController.Initialize(_playerConfig.MaxHealth);
        }

        public void StartMoving()
        {
            _movementController.Initialize(_playerConfig.Speed, _currentLevel.SplineContainer, _currentLevel.EndPoint.position, OnWin);
            _weaponController.Enable(true);
            _healthBarManager.Spawn(_healthController, HealthBarType.Player);
        }
        
        private void OnWin()
        {
            Restart();
            LevelFinished?.Invoke(true);
        }

        private void OnDied()
        {
            Restart();
            LevelFinished?.Invoke(false);
        }

        public void Restart()
        {
            _healthBarManager.Remove(_healthController, HealthBarType.Player);
            _movementController.Stop();
            _weaponController.Enable(false);
        }
    }
}