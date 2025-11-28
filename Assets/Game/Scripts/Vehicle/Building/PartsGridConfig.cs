using UnityEngine;

namespace Vehicle.Building
{
    [CreateAssetMenu(fileName = "PartsGridConfig", menuName = "Configs/Vehicle/Parts Grid")]
    public class PartsGridConfig : ScriptableObject
    {
        [field: SerializeField] public PartsGridSize Size { get; private set; }
    }
}