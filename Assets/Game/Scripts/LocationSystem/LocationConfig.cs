using LevelSystem;
using UnityEngine;

namespace LocationSystem
{
    [CreateAssetMenu(fileName = "LocationConfig", menuName = "Configs/Location")]
    public class LocationConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public LevelConfig[] LevelConfigs { get; private set; }
    }
}