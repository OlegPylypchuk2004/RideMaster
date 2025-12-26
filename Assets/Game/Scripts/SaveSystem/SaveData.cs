using PerkSystem;
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
        public List<PerkData> availablePerks;

        public int dailyRewardsDaysClaimedCount;
        public int dailyRewardsDayNumber;

        public SaveData()
        {
            currencies = new Dictionary<string, int>();

            availablePartsCollection = new List<PartConfig>();
            availablePerks = new List<PerkData>();

            dailyRewardsDaysClaimedCount = 0;
            dailyRewardsDayNumber = 1;
        }
    }
}