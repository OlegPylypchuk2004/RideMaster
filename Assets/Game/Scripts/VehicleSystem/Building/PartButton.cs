using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VehicleSystem.Parts;

namespace VehicleSystem.Building
{
    public class PartButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _enabledDisplay;
        [SerializeField] private GameObject _disbledDisplay;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _countTextMesh;

        private PartData _partData;

        public PartData PartData => _partData;

        public event Action<PartButton> Selected;

        private void Awake()
        {
            Disable();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Selected?.Invoke(this);
        }

        public void SetPartData(PartData partData)
        {
            _partData = partData;

            if (_partData == null)
            {
                Disable();

                return;
            }

            Enable();

            _iconImage.sprite = _partData.config.IconSprite;

            UpdatePartsCountText();
        }

        public void IncreasePartsCount()
        {
            _partData.count++;

            UpdatePartsCountText();
        }

        public void ReducePartsCount()
        {
            if (_partData.count > 0)
            {
                _partData.count--;
            }

            UpdatePartsCountText();
        }

        public void Enable()
        {
            _enabledDisplay.SetActive(true);
            _disbledDisplay.SetActive(false);
        }

        public void Disable()
        {
            _enabledDisplay.SetActive(false);
            _disbledDisplay.SetActive(true);
        }

        private void UpdatePartsCountText()
        {
            if (_partData == null)
            {
                return;
            }

            _countTextMesh.text = $"x{_partData.count}";
        }
    }
}