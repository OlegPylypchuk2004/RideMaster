using UnityEngine;

namespace Vehicle.Part.Configs
{
    [CreateAssetMenu(fileName = "PartConfig", menuName = "Configs/Vehicle/Part")]
    public class PartConfig : ScriptableObject
    {
        [field: SerializeField] public VehiclePartPreview PreviewPrefab { get; private set; }
        [field: SerializeField] public VehiclePartGameplay GameplayPrefab { get; private set; }
    }
}