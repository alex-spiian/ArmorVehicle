using UnityEngine;
using UnityEngine.Splines;

namespace ArmorVehicle
{
    public class Level : MonoBehaviour
    {
        [field:SerializeField] public SplineContainer SplineContainer { get; private set; }
        [field:SerializeField] public LevelZone[] Zones { get; private set; }
        [field:SerializeField] public Transform StartPoint { get; private set; }
        [field:SerializeField] public Transform EndPoint { get; private set; }
    }
}