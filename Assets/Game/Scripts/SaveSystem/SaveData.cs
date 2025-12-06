using System;
using System.Collections.Generic;
using VehicleSystem.Parts;

namespace SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public Dictionary<string, int> currencies;

        public List<PartConfig> availablePartsCollection;

        public int dailyRewardsDaysClaimedCount;
        public int dailyRewardsDayNumber;

        public SaveData()
        {
            currencies = new Dictionary<string, int>();

            availablePartsCollection = new List<PartConfig>();

            dailyRewardsDaysClaimedCount = 0;
            dailyRewardsDayNumber = 1;
        }
    }
}