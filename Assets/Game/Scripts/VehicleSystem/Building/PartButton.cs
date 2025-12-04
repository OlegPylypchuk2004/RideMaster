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
        [SerializeField] private GameObject _disabledDisplay;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _countTextMesh;

        private PartData _partData;

        public PartData PartData => _partData;

        public event Action<PartButton> Selected;

        private void Awake()
        {
            Disable();
        }

        private void OnDestroy()
        {
            if (_partData != null)
            {
                _partData.CountChanged -= OnCountChanged;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Selected?.Invoke(this);
        }

        public void SetPartData(PartData partData)
        {
            if (_partData != null)
            {
                _partData.CountChanged -= OnCountChanged;
            }

            _partData = partData;

            if (_partData == null)
            {
                Disable();
                return;
            }

            _partData.CountChanged += OnCountChanged;

            Enable();
            UpdateIcon();
            UpdateCountText();
        }

        public void Enable()
        {
            _enabledDisplay.SetActive(true);
            _disabledDisplay.SetActive(false);
        }

        public void Disable()
        {
            _enabledDisplay.SetActive(false);
            _disabledDisplay.SetActive(true);
        }

        private void UpdateIcon()
        {
            if (_partData == null)
            {
                return;
            }

            if (_partData.Config == null)
            {
                return;
            }

            _iconImage.sprite = _partData.Config.IconSprite;
        }

        private void UpdateCountText()
        {
            if (_partData == null)
            {
                return;
            }

            _countTextMesh.text = $"x{_partData.Count}";
        }

        private void OnCountChanged(int count)
        {
            UpdateCountText();
        }
    }
}