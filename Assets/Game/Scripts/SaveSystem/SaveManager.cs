using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager
    {
        public string FilePath => Path.Combine(Application.persistentDataPath, "SaveData.json");

        private SaveData _cachedData;

        public SaveData Data
        {
            get
            {
                if (_cachedData == null)
                {
                    Load();
                }

                return _cachedData;
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(_cachedData, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                _cachedData = JsonConvert.DeserializeObject<SaveData>(json);

                if (_cachedData == null)
                {
                    _cachedData = new SaveData();
                    Save();
                }
            }
            else
            {
                _cachedData = new SaveData();
                Save();
            }
        }

        public void Delete()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            _cachedData = new SaveData();
        }
    }
}