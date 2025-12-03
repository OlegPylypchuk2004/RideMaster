using UnityEngine;
using VehicleSystem.Parts;
using VehicleSystem.Parts.Preview;

namespace VehicleSystem.Building
{
    public class PartSection : MonoBehaviour
    {
        [field: SerializeField] public Transform BuildPoint { get; private set; }
        [field: SerializeField] public PartsGridSize Position { get; private set; }

        public PartConfig PartConfig { get; private set; }
        public PreviewPart PartPreview { get; private set; }

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

        public bool TryRemovePart()
        {
            if (IsEmpty())
            {
                return false;
            }

            Destroy(PartPreview.gameObject);
            PartConfig = null;
            PartPreview = null;

            return true;
        }

        public bool IsEmpty()
        {
            return PartConfig == null && PartPreview == null;
        }
    }
}