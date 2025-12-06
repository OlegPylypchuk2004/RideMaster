namespace DailyRewardsSystem
{
    public class DailyRewards
    {
        public int DaysClaimedCount => 0;
        public int DayNumber => 1;

        public bool IsCanClaimReward(DayConfig dayConfig)
        {
            if (dayConfig == null)
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
    }
}