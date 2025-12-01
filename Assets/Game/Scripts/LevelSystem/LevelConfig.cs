using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int Number { get; private set; }
    }
}