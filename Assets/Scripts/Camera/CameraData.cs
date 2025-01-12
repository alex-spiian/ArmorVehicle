using System;
using Cinemachine;
using UnityEngine;

namespace ArmorVehicle
{
    [Serializable]
    public class CameraData
    {
        [field:SerializeField] public CameraType Type { get; private set; }
        [field:SerializeField] public CinemachineVirtualCamera Camera { get; private set; }
    }
}