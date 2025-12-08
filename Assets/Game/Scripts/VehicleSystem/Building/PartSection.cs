using System;
using UnityEngine;
using VehicleSystem.Parts;
using VehicleSystem.Parts.Preview;

namespace VehicleSystem.Building
{
    public class PartSection : MonoBehaviour
    {
        private PartSection _topSection;
        private PartSection _bottomSection;
        private PartSection _rightSection;
        private PartSection _leftSection;

        public PartConfig PartConfig { get; private set; }
        public PreviewPart PartPreview { get; private set; }

        public event Action<PartSection> Selected;

        private void OnMouseDown()
        {
            Selected?.Invoke(this);
        }

        public void Initialize(PartSection topSection, PartSection bottomSection, PartSection rightSection, PartSection leftSection)
        {
            _topSection = topSection;
            _bottomSection = bottomSection;
            _rightSection = rightSection;
            _leftSection = leftSection;
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

        public void UpdatePartRotation()
        {
            if (IsEmpty())
            {
                return;
            }

            //
        }
    }
}