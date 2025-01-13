using UnityEngine;

namespace ArmorVehicle
{
    public abstract class EntityConfig : ScriptableObject
    {
        [field:SerializeField] public float MaxHealth { get; private set; }
        [field:SerializeField] public float Damage { get; private set; }
        [field:SerializeField] public float AttackCooldown { get; private set; }
        [field:SerializeField] public float Speed { get; private set; }
    }
}