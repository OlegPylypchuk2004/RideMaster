using UnityEngine;
using VehicleSystem.Parts.Preview;

namespace VehicleSystem.Parts
{
    [CreateAssetMenu(fileName = "PartConfig", menuName = "Configs/Vehicle/Part")]
    public class PartConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public Sprite IconSprite { get; private set; }
        [field: SerializeField] public Sprite LockedIconSprite { get; private set; }
        [field: SerializeField] public PreviewPart PreviewPrefab { get; private set; }
        [field: SerializeField] public GameplayPart GameplayPrefab { get; private set; }
        [field: SerializeField] public DestroyedPart DestroyPrefab { get; private set; }

        [field: Space(25f)]
        [field: SerializeField, Min(0)] public int Strength { get; private set; }
        [field: SerializeField, Min(0)] public int Mass { get; private set; }
    }
}