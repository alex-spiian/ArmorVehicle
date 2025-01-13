using UnityEngine;

namespace ArmorVehicle.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameControllerConfig", fileName = "GameControllerConfig")]
    public class GameControllerConfig : ScriptableObject
    {
        [field:SerializeField] public PlayerController PlayerControllerPrefab { get; private set; }
        [field:SerializeField] public LevelList LevelList { get; private set; }
        [field:SerializeField] public EnemySpawnerConfig EnemySpawnerConfig { get; private set; }
        [field:SerializeField] public PlayerConfig PlayerConfig { get; private set; }
    }
}