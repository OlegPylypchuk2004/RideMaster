using RoadSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using VehicleSystem.Building;
using VehicleSystem.Parts;

namespace LevelSystem
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField, MinValue(0)] public int Number { get; private set; }
        [field: SerializeField] public Road RoadPrefab { get; private set; }
        [field: SerializeField] public PartsGridConfig PartsGridConfig { get; private set; }
        [field: SerializeField] public PartData[] PartDatas { get; private set; }
    }
}