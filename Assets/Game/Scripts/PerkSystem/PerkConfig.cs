using UnityEngine;

namespace PerkSystem
{
    [CreateAssetMenu(fileName = "PerkConfig", menuName = "Configs/Perk")]
    public class PerkConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
    }
}