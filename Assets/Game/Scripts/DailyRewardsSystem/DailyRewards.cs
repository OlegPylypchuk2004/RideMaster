using SaveSystem;
using System;
using WalletSystem;

namespace DailyRewardsSystem
{
    public class DailyRewards
    {
        private readonly SaveManager _saveManager;
        private readonly CurrencyWallet _currencyWallet;

        public event Action<DayConfig> RewardClaimed;

        public DailyRewards(SaveManager saveManager, CurrencyWallet currencyWallet)
        {
            _saveManager = saveManager;
            _currencyWallet = currencyWallet;
        }

        public int DaysClaimedCount => _saveManager.Data.dailyRewardsDaysClaimedCount;
        public int DayNumber => _saveManager.Data.dailyRewardsDayNumber;

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

                _saveManager.Data.dailyRewardsDaysClaimedCount++;
                _saveManager.Data.dailyRewardsDayNumber++;
                _saveManager.Save();

                RewardClaimed?.Invoke(dayConfig);

                return true;
            }

            return false;
        }
    }
}