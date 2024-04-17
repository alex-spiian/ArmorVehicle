using System;
using UnityEngine;

namespace Road
{
    [Serializable]
    public class RoadSizeProvider
    {
        [field:SerializeField] public Transform LeftUpper { get; private set; }
        [field:SerializeField] public Transform RightUpper { get; private set; }
        [field:SerializeField] public Transform RightLower { get; private set; }
        [field:SerializeField] public Transform LeftLower { get; private set; }
    }
}