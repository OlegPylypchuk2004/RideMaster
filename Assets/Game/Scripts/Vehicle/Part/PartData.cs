using System;
using UnityEngine;

namespace Vehicle.Part
{
    [Serializable]
    public class PartData
    {
        [field: SerializeField] public PartConfig PartConfig { get; private set; }
        [field: SerializeField, Min(0)] public int Count { get; private set; }
    }
}