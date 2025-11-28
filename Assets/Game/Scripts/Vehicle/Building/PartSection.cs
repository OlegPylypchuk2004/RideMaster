using UnityEngine;
using Vehicle.Part;
using Vehicle.Part.Configs;

namespace Vehicle.Building
{
    public class PartSection : MonoBehaviour
    {
        private PartConfig _partConfig;
        private PartPreview _partPreview;

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

            _partConfig = partConfig;
            _partPreview = Instantiate(partConfig.PreviewPrefab, transform);

            return true;
        }

        public bool IsEmpty()
        {
            return _partConfig == null && _partPreview == null;
        }
    }
}