using UnityEngine;

namespace ArmorVehicle
{
    public class LevelZone : MonoBehaviour
    {
        [field:SerializeField] public Transform CenterPoint { get; private set; }
        [field:SerializeField] public Vector2 Size { get; private set; }
        [field: SerializeField] public bool ShouldSpawnEnemies { get; private set; }

        private Color _gizmoColor = Color.yellow;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireCube(CenterPoint.position, new Vector3(Size.x, 0.1f, Size.y));
        }
#endif
    }
}