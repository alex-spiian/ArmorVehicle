using System;
using UnityEngine;

namespace ArmorVehicle
{
    [Serializable]
    public class FxData
    {
        [field:SerializeField] public FxType Type { get; private set; }
        [field:SerializeField] public GameObject Effect { get; private set; }
    }
}