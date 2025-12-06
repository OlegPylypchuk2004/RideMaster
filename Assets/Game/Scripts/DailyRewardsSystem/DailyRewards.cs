using System;
using WalletSystem;

namespace DailyRewardsSystem
{
    public class DailyRewards
    {
        private readonly CurrencyWallet _currencyWallet;

        public event Action<DayConfig> RewardClaimed;

        public DailyRewards(CurrencyWallet currencyWallet)
        {
            _currencyWallet = currencyWallet;
        }

        public int DaysClaimedCount => 0;
        public int DayNumber => 1;

        public bool IsCanClaimReward(DayConfig dayConfig)
        {
            if (dayConfig == null)
            {
                return false;
            }

            if (IsRewardClaimed(dayConfig))
            {
                return false;
            }

            return dayConfig.Number == 1;
        }

        public bool IsRewardClaimed(DayConfig dayConfig)
        {
            if (dayConfig == null)
            {
                return false;
            }

            return false;
        }

        public bool TryClaim(DayConfig dayConfig)
        {
            if (dayConfig == null)
            {
                return false;
            }

            if (!IsCanClaimReward(dayConfig))
            {
                return false;
            }

            DayReward dayReward = dayConfig.Reward;

            if (dayReward is CurrencyDayReward ñurrencyDayReward)
            {
                _currencyWallet.TryIncrease(ñurrencyDayReward.Data);

                RewardClaimed?.Invoke(dayConfig);

                return true;
            }

            return false;
        }
    }
}