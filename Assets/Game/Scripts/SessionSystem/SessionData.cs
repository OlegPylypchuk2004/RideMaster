using LevelSystem;
using LocationSystem;

namespace SessionSystem
{
    public class SessionData
    {
        public LocationConfig locationConfig;
        public LevelConfig levelConfig;

        public SessionData(LocationConfig defaultLocationConfig, LevelConfig defaultLevelConfig)
        {
            locationConfig = defaultLocationConfig;
            levelConfig = defaultLevelConfig;
        }
    }
}