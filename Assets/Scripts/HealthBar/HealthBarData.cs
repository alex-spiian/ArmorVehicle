using System;
using UnityEngine;

namespace HealthBar
{
    [Serializable]
    public class HealthBarData
    {
        [field:SerializeField] public HealthBarType Type { get; private set; }
        [field:SerializeField] public HealthBar Prefab { get; private set; }
    }
}