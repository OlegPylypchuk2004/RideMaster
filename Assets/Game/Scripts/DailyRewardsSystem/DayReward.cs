using System;
using UnityEngine;

namespace DailyRewardsSystem
{
    [Serializable]
    public abstract class DayReward
    {
        public abstract Sprite GetIcon();
        public abstract string GetText();
    }
}