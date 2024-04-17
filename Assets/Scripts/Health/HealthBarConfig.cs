using UnityEngine;

namespace Health
{
    [CreateAssetMenu(menuName = "ScriptableObject/HealthBarConfig")]
    public class HealthBarConfig : ScriptableObject
    {
        [field:SerializeField] public HealthBarPositionController Prefab { get; private set; }
        [field:SerializeField] public RectTransform Canvas { get; private set; }
    }
}