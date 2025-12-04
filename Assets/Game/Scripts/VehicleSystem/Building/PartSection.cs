using System;
using UnityEngine;
using VehicleSystem.Parts;
using VehicleSystem.Parts.Preview;

namespace VehicleSystem.Building
{
    public class PartSection : MonoBehaviour
    {
        public PartConfig PartConfig { get; private set; }
        public PreviewPart PartPreview { get; private set; }

        public event Action<PartSection> Selected;

        private void OnMouseDown()
        {
            Selected?.Invoke(this);
        }

        public bool TrySetPart(PartConfig partConfig)
        {
            if (!IsEmpty())
            {
                return false;
            }

            if (partConfig == null)
            {
                return false;
            }

            if (partConfig.PreviewPrefab == null)
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

            PartPreview = null;
            PartConfig = null;

            return true;
        }

        public bool IsEmpty()
        {
            return PartConfig == null && PartPreview == null;
        }
    }
}