using Camera;
using Car;
using Health;
using Level;
using ScreenController;
using UnityEngine;
using Weapon.Turret;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Car.Car _car;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private TurretController _turretController;
        [SerializeField] private CameraPositionController _cameraPositionController;

        [SerializeField] private ScreensSwitcher _screensSwitcher;
        [SerializeField] private HealthBarConfig _healthBarConfig;
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private CarVisualEffector _carVisualEffector;


        private void Awake()
        {
            _car.Initialize(_healthBarConfig.Prefab, _canvas);
            _car.Lost += OnLost;
            _car.Won += OnWon;
            StartGame();
        }

        private void OnLost()
        {
            _cameraPositionController.OnGameOver();
            _screensSwitcher.ShowDefeatScreen();
            _screensSwitcher.ShowSplashScreen();
            _turretController.Block();
            _levelController.WaitToContinue(OnRestarted);
        }

        private void StartGame()
        {
            _screensSwitcher.ShowSplashScreen();
            _levelController.WaitToContinue(OnRestarted);
        }
        private void OnWon()
        {
            _cameraPositionController.OnGameOver();
            _screensSwitcher.ShowVictoryScreen();
            _screensSwitcher.ShowSplashScreen();
            _turretController.Block();
            _levelController.WaitToContinue(OnRestarted);
        }

        private void OnRestarted()
        {
            _cameraPositionController.OnGameStarted();
            _turretController.UnBlock();
            _screensSwitcher.HideAll();
            _carVisualEffector.OnRestarted();
        }

        private void OnDestroy()
        {
            _car.Lost -= OnLost;
            _car.Won -= OnWon;
        }
    }
}