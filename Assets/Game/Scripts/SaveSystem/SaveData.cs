using System;
using System.Collections.Generic;

namespace SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public Dictionary<string, int> currencies;

        public int dailyRewardsDaysClaimedCount;
        public int dailyRewardsDayNumber;

        public SaveData()
        {
            currencies = new Dictionary<string, int>();

            dailyRewardsDaysClaimedCount = 0;
            dailyRewardsDayNumber = 1;
        }
    }
}