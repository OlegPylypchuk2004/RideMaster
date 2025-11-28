using UnityEngine;
using Vehicle.Part;
using Vehicle.Part.Configs;

namespace Vehicle.Building
{
    public class PartSection : MonoBehaviour
    {
        [field: SerializeField] public Transform BuildPoint { get; private set; }

        public PartConfig PartConfig { get; private set; }
        public VehiclePartPreview PartPreview { get; private set; }

        public bool TrySetPart(PartConfig partConfig)
        {
            if (!IsEmpty())
            {
                return false;
            }

            if (partConfig == null || partConfig.PreviewPrefab == null)
            {
                return false;
            }

            PartConfig = partConfig;
            PartPreview = Instantiate(partConfig.PreviewPrefab, transform);

            return true;
        }

        public bool IsEmpty()
        {
            return PartConfig == null && PartPreview == null;
        }
    }
}