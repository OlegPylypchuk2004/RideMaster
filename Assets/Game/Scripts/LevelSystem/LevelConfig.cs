using RoadSystem;
using UnityEngine;
using Vehicle.Part;

namespace LevelSystem
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int Number { get; private set; }
        [field: SerializeField] public Road RoadPrefab { get; private set; }
        [field: SerializeField] public PartData[] PartDatas { get; private set; }
    }
}