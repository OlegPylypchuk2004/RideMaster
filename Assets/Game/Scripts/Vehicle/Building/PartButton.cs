using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vehicle.Part;

namespace Vehicle.Building
{
    public class PartButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _enabledDisplay;
        [SerializeField] private GameObject _disbledDisplay;
        [SerializeField] private Image _iconImage;

        private PartData _partData;

        public event Action<PartData> Selected;

        private void Awake()
        {
            Disable();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Selected?.Invoke(_partData);
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
    }
}