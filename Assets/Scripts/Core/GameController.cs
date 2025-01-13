using System;
using ArmorVehicle.Ui;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace ArmorVehicle.Core
{
    public class GameController : IStartable, IDisposable
    {
        private EnemySpawner _enemySpawner;
        private LevelSwitcher _levelSwitcher;
        private PlayerController _playerController;
        private GameControllerConfig _gameControllerConfig;
        private IObjectResolver _container;
        private CameraController _cameraController;

        [Inject]
        public void Construct(
            GameControllerConfig gameControllerConfig,
            LevelSwitcher levelSwitcher,
            HealthBarManager healthBarManager,
            ScreensManager screensManager,
            CameraController cameraController,
            IObjectResolver container)
        {
            _cameraController = cameraController;
            _container = container;
            _gameControllerConfig = gameControllerConfig;
            _levelSwitcher = levelSwitcher;
            
            screensManager.Initialize();
            _levelSwitcher.Initialize(_gameControllerConfig.LevelList);
            _playerController = CreatePlayer();
            _cameraController.Initialize(_playerController.CameraTarget);
            _enemySpawner = new EnemySpawner(_gameControllerConfig.EnemySpawnerConfig, healthBarManager, _playerController.HealthHandler);
            _playerController.LevelFinished += OnLevelFinished;
        }
        
        public void Dispose()
        {
            _playerController.LevelFinished -= OnLevelFinished;
        }
        
        public async void Start()
        {
            var taskCompetitionSource = new UniTaskCompletionSource<bool>();
            ScreensManager.OpenScreenAsync<TutorialScreen, TutorialScreenContext>(new TutorialScreenContext(), taskCompetitionSource);
            
            _levelSwitcher.Next();
            _enemySpawner.Spawn(_levelSwitcher.CurrentLevel, _levelSwitcher.CurrentLevelIndex);
            _playerController.Initialize(_levelSwitcher.CurrentLevel, _gameControllerConfig.PlayerConfig);

            await taskCompetitionSource.Task;
            ScreensManager.CloseScreen<TutorialScreen>();
            _cameraController.Switch(CameraType.Main);
            _playerController.StartMoving();
        }

        private async void OnLevelFinished(bool isVictory)
        {
            await HandleScreensFlow(isVictory);
            _cameraController.Switch(CameraType.Idle);
            _playerController.Restart();
            _enemySpawner.Spawn(_levelSwitcher.CurrentLevel, _levelSwitcher.CurrentLevelIndex);
            _playerController.Initialize(_levelSwitcher.CurrentLevel, _gameControllerConfig.PlayerConfig);
            
            await WaitForStart();
            ScreensManager.CloseScreen<TutorialScreen>();
            _cameraController.Switch(CameraType.Main);
            _playerController.StartMoving();
        }

        private async UniTask HandleScreensFlow(bool isVictory)
        {
            _enemySpawner.KillAll();
            var taskCompetitionSource = new UniTaskCompletionSource<bool>();

            switch (isVictory)
            {
                case true:
                    ScreensManager.OpenScreenAsync<VictoryScreen, VictoryScreenContext>(new VictoryScreenContext(),
                        taskCompetitionSource);
                    _levelSwitcher.Next();
                    break;
                
                case false:
                    ScreensManager.OpenScreenAsync<DefeatScreen, DefeatScreenContext>(new DefeatScreenContext(),
                        taskCompetitionSource);
                    _levelSwitcher.Restart();
                    break;
            }

            await taskCompetitionSource.Task;
            ScreensManager.CloseAllScreens();
        }

        private async UniTask WaitForStart()
        {
            var taskCompetitionSource = new UniTaskCompletionSource<bool>();
            ScreensManager.OpenScreenAsync<TutorialScreen, TutorialScreenContext>(new TutorialScreenContext(), 
                taskCompetitionSource);
            
            await taskCompetitionSource.Task;
        }

        private PlayerController CreatePlayer()
        {
            return _container.Instantiate(_gameControllerConfig.PlayerControllerPrefab);
        }
    }
}