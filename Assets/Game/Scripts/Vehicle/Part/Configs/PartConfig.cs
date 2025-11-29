using UnityEngine;

namespace Vehicle.Part.Configs
{
    [CreateAssetMenu(fileName = "PartConfig", menuName = "Configs/Vehicle/Part")]
    public class PartConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite IconSprite { get; private set; }
        [field: SerializeField] public PreviewPart PreviewPrefab { get; private set; }
        [field: SerializeField] public GameplayPart GameplayPrefab { get; private set; }
    }
}