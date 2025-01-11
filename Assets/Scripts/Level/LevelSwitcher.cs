using UnityEngine;
using VContainer;

namespace ArmorVehicle
{
    public class LevelSwitcher
    {
        public int CurrentLevelIndex { get; private set; }
        public Level CurrentLevel { get; private set; }

        private LevelList _levelList;

        [Inject]
        public void Construct(LevelList levelList)
        {
            _levelList = levelList;
        }

        public void Next()
        {
            if (CurrentLevelIndex >= _levelList.Levels.Length)
            {
                return;
            }

            RemovePrevious();
            
            Spawn();
            CurrentLevelIndex++;
        }

        public void Restart()
        {
            RemovePrevious();
            Spawn();
        }
        
        private void Spawn()
        {
            var currentLevelPrefab = _levelList.Levels[CurrentLevelIndex];
            CurrentLevel = Object.Instantiate(currentLevelPrefab);
        }

        private void RemovePrevious()
        {
            if (CurrentLevel != null)
            {
                Object.Destroy(CurrentLevel.gameObject);
            }
        }
    }
}