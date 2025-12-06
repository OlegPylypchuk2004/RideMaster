using System;
using UnityEngine;
using WalletSystem;

namespace DailyRewardsSystem
{
    [Serializable]
    public class CurrencyDayReward : DayReward
    {
        [field: SerializeField] public WalletOperationData Data { get; private set; }

        public override Sprite GetIcon()
        {
            return Data.currencyConfig.Icon;
        }

        public override string GetText()
        {
            return $"x{Data.count}";
        }
    }
}