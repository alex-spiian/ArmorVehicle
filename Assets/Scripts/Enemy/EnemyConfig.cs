using UnityEngine;

namespace ArmorVehicle
{
    [CreateAssetMenu(menuName = "ScriptableObject/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : EntityConfig
    {
        [field:SerializeField] public float AttackDistance{ get; private set; }
        [field:SerializeField] public float TargetDetectionDistance{ get; private set; }
    }
}