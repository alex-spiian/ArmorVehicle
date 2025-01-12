using System;
using HealthBar;
using UnityEngine;
using VContainer;

namespace ArmorVehicle
{
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(WeaponController))]
    [RequireComponent(typeof(HealthController))]
    public class PlayerController : MonoBehaviour
    {
        private WeaponController _weaponController;
        private MovementController _movementController;
        private HealthController _healthController;
        private IInputHandler _inputHandler;
        private HealthBarManager _healthBarManager;

        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
            _weaponController = GetComponent<WeaponController>();
            _healthController = GetComponent<HealthController>();
        }

        [Inject]
        public void Construct(IInputHandler inputHandler, HealthBarManager healthBarManager)
        {
            _healthBarManager = healthBarManager;
            _inputHandler = inputHandler;
        }

        public void Initialize(Level level, Action<bool> levelFinishedCallBack)
        {
            _movementController.Initialize(level.SplineContainer, level.EndPoint.position, levelFinishedCallBack);
            _weaponController.Initialize(_inputHandler);
            _healthController.Initialize(100);
            _healthBarManager.Spawn(_healthController, HealthBarType.Player);
        }

        public void Restart()
        {
            _healthBarManager.Remove(_healthController);
            _movementController.Stop();
        }
    }
}