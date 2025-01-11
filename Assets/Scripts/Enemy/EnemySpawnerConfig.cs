using UnityEngine;

namespace ArmorVehicle
{
    [CreateAssetMenu(menuName = "ScriptableObject/EnemySpawnerConfig", fileName = "EnemySpawnerConfig")]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [field:SerializeField] public Enemy EnemyPrefab { get; private set; }
        [field:SerializeField] public int MinEnemiesOnZone { get; private set; }
        
        [field:SerializeField, Range(0.1f, 1f)] 
        public float EnemyIncreasePercentPerLevel { get; private set; }
        [field:SerializeField, Range(0.1f, 1f)] 
        public float EnemyIncreasePercentPerZone { get; private set; }
    }
}