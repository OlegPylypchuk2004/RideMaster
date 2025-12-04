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

            if (PartConfig.Type == PartType.Attachable)
            {
                if (_topSection != null && !_topSection.IsEmpty() && _topSection.PartConfig.Type == PartType.Base)
                {
                    Vector3 targetRotation = PartPreview.transform.rotation.eulerAngles;
                    targetRotation.x = 0f;
                    PartPreview.transform.rotation = Quaternion.Euler(targetRotation);
                }
                else if (_bottomSection != null && !_bottomSection.IsEmpty() && _bottomSection.PartConfig.Type == PartType.Base)
                {
                    Vector3 targetRotation = PartPreview.transform.rotation.eulerAngles;
                    targetRotation.x = 180f;
                    PartPreview.transform.rotation = Quaternion.Euler(targetRotation);
                }
                else if (_rightSection != null && !_rightSection.IsEmpty() && _rightSection.PartConfig.Type == PartType.Base)
                {
                    Vector3 targetRotation = PartPreview.transform.rotation.eulerAngles;
                    targetRotation.x = 270f;
                    PartPreview.transform.rotation = Quaternion.Euler(targetRotation);
                }
                else if (_leftSection != null && !_leftSection.IsEmpty() && _leftSection.PartConfig.Type == PartType.Base)
                {
                    Vector3 targetRotation = PartPreview.transform.rotation.eulerAngles;
                    targetRotation.x = 90f;
                    PartPreview.transform.rotation = Quaternion.Euler(targetRotation);
                }
                else
                {
                    Vector3 targetRotation = PartPreview.transform.rotation.eulerAngles;
                    targetRotation.x = 0f;
                    PartPreview.transform.rotation = Quaternion.Euler(targetRotation);
                }
            }
        }
    }
}