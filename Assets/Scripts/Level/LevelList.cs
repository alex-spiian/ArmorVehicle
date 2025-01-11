using UnityEngine;

namespace ArmorVehicle
{
    [CreateAssetMenu(menuName = "ScriptableObject/LevelList", fileName = "LevelList")]
    public class LevelList : ScriptableObject
    {
        [field:SerializeField] public Level[] Levels { get; private set; }
    }
}