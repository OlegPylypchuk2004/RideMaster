namespace DailyRewardsSystem
{
    public class DailyRewards
    {
        public bool IsCanClaimReward(DayConfig dayConfig)
        {
            if (dayConfig == null)
            {
                return false;
            }

            return dayConfig.Number == 1;
        }
    }
}