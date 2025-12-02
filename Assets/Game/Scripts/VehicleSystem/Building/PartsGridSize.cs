using System;
using UnityEngine;

namespace VehicleSystem.Building
{
    [Serializable]
    public struct PartsGridSize
    {
        [Min(0)] public int rows;
        [Min(0)] public int columns;
    }
}