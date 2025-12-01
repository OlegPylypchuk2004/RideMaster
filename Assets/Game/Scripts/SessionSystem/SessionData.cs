using LevelSystem;

namespace SessionSystem
{
    public class SessionData
    {
        public LevelConfig levelConfig;

        public SessionData(LevelConfig defaultLevelConfig)
        {
            levelConfig = defaultLevelConfig;
        }
    }
}