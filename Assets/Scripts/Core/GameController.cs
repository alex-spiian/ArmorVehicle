using System.Threading.Tasks;
using ArmorVehicle.Ui;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace ArmorVehicle.Core
{
    public class GameController : IStartable
    {
        private EnemySpawner _enemySpawner;
        private LevelSwitcher _levelSwitcher;
        private PlayerController _playerController;
        private GameControllerConfig _gameControllerConfig;
        private IObjectResolver _container;

        [Inject]
        public void Construct(
            GameControllerConfig gameControllerConfig,
            LevelSwitcher levelSwitcher,
            HealthBarManager healthBarManager,
            ScreensManager screensManager,
            IObjectResolver container)
        {
            _container = container;
            _gameControllerConfig = gameControllerConfig;
            _levelSwitcher = levelSwitcher;
            
            screensManager.Initialize();
            _levelSwitcher.Initialize(_gameControllerConfig.LevelList);
            _playerController = CreatePlayer();
            _enemySpawner = new EnemySpawner(_gameControllerConfig.EnemySpawnerConfig, healthBarManager, _playerController.HealthHandler);
        }
        
        public async void Start()
        {
            var taskCompetitionSource = new UniTaskCompletionSource<bool>();
            ScreensManager.OpenScreenAsync<TutorialScreen, TutorialScreenContext>(new TutorialScreenContext(), taskCompetitionSource);
            
            _levelSwitcher.Next();
            _enemySpawner.Spawn(_levelSwitcher.CurrentLevel, _levelSwitcher.CurrentLevelIndex);
            _playerController.Initialize(_levelSwitcher.CurrentLevel);

            await taskCompetitionSource.Task;
            ScreensManager.CloseScreen<TutorialScreen>();
            _playerController.StartMoving(OnLevelFinished);
        }

        private async void OnLevelFinished(bool isVictory)
        {
            await HandleScreensFlow(isVictory);

            _playerController.Restart();
            _enemySpawner.Spawn(_levelSwitcher.CurrentLevel, _levelSwitcher.CurrentLevelIndex);
            _playerController.Initialize(_levelSwitcher.CurrentLevel);
            
            await WaitForStart();
            ScreensManager.CloseScreen<TutorialScreen>();
            _playerController.StartMoving(OnLevelFinished);
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

        private async Task WaitForStart()
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