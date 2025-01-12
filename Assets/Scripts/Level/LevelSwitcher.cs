using UnityEngine;

namespace ArmorVehicle
{
    public class LevelSwitcher
    {
        public int CurrentLevelIndex { get; private set; }
        public Level CurrentLevel { get; private set; }

        private LevelList _levelList;

        public void Initialize(LevelList levelList)
        {
            _levelList = levelList;
        }

        public void Next()
        {
            if (CurrentLevelIndex >= _levelList.Levels.Length)
            {
                Restart();
                return;
            }

            RemovePrevious();
            Spawn();
            CurrentLevelIndex++;
        }

        public void Restart()
        {
            CurrentLevelIndex--;
            Next();
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