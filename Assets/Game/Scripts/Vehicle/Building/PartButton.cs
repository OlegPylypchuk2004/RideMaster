using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vehicle.Part;

namespace Vehicle.Building
{
    public class PartButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private PartConfig _partConfig;
        [SerializeField] private GameObject _enabledDisplay;
        [SerializeField] private GameObject _disbledDisplay;
        [SerializeField] private Image _iconImage;

        public event Action<PartConfig> Selected;

        private void Awake()
        {
            if (_partConfig == null)
            {
                _enabledDisplay.SetActive(false);
                _disbledDisplay.SetActive(true);

                return;
            }

            _enabledDisplay.SetActive(true);
            _disbledDisplay.SetActive(false);

            _iconImage.sprite = _partConfig.IconSprite;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Selected?.Invoke(_partConfig);
        }
    }
}