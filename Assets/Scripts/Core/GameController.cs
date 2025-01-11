using UnityEngine;
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

        [Inject]
        public void Construct(
            GameControllerConfig gameControllerConfig,
            LevelSwitcher levelSwitcher)
        {
            _gameControllerConfig = gameControllerConfig;
            _levelSwitcher = levelSwitcher;
            _levelSwitcher.Initialize(_gameControllerConfig.LevelList);
            _enemySpawner = new EnemySpawner(_gameControllerConfig.EnemySpawnerConfig);
            _playerController = CreatePlayer();
        }
        
        public void Start()
        {
            _levelSwitcher.Next();
            _enemySpawner.SpawnEnemiesInZone(_levelSwitcher.CurrentLevel, _levelSwitcher.CurrentLevelIndex);
            _playerController.Initialize(_levelSwitcher.CurrentLevel, OnLevelFinished);
        }

        private void OnLevelFinished(bool isVictory)
        {
            Debug.Log("Victory");
            _levelSwitcher.Restart();
            _playerController.Initialize(_levelSwitcher.CurrentLevel, OnLevelFinished);
            _enemySpawner.SpawnEnemiesInZone(_levelSwitcher.CurrentLevel, _levelSwitcher.CurrentLevelIndex);
        }

        private PlayerController CreatePlayer()
        {
            return Object.Instantiate(_gameControllerConfig.PlayerControllerPrefab);
        }
    }
}