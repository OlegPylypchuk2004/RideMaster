using System;
using UnityEngine;
using WalletSystem;

namespace DailyRewardsSystem
{
    [Serializable]
    public class CurrencyDayReward : DayReward
    {
        [field: SerializeField] public CurrencyConfig CurrencyConfig { get; private set; }
        [field: SerializeField, Min(0)] public int Count { get; private set; }

        public override Sprite GetIcon()
        {
            return CurrencyConfig.Icon;
        }

        public override string GetText()
        {
            return $"x{Count}";
        }
    }
}