using UnityEngine;

namespace ArmorVehicle
{
    public class LevelZone : MonoBehaviour
    {
        public Transform Centerpoint;
        public Vector2 Size;
    
        private Color _gizmoColor = Color.yellow;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireCube(Centerpoint.position, new Vector3(Size.x, 0.1f, Size.y));
        }
#endif
    }
}