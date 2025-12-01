using System;
using System.Collections.Generic;

namespace SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public Dictionary<string, int> currencies;

        public SaveData()
        {
            currencies = new Dictionary<string, int>();
        }
    }
}