using Sirenix.OdinInspector;
using System;

namespace VehicleSystem.Building
{
    [Serializable]
    public struct PartsGridSize
    {
        [MinValue(0)] public int rows;
        [MinValue(0)] public int columns;
    }
}