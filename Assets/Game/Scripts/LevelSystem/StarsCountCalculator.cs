using UnityEngine;
using VehicleSystem;

namespace LevelSystem
{
    public class StarsCountCalculator
    {
        private const int MinStarsCount = 0;
        private const int MaxStarsCount = 3;

        public int Calculate(Vehicle vehicle)
        {
            float totalParts = vehicle.PartsCount;
            float destroyedParts = vehicle.DestroyedPartsCount;
            float intactParts = totalParts - destroyedParts;
            float healthNormalized = Mathf.Clamp01(intactParts / totalParts);
            int starsCount = Mathf.RoundToInt(healthNormalized * MaxStarsCount);

            return Mathf.Clamp(starsCount, MinStarsCount, MaxStarsCount);
        }
    }
}