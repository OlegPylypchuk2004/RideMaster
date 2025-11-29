using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vehicle.Part.Configs;

namespace Vehicle.Part.UI
{
    public class VehiclePartButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private PartConfig _partConfig;
        [SerializeField] private Image _iconImage;

        public event Action<PartConfig> Selected;

        private void Awake()
        {
            if (_partConfig == null)
            {
                return;
            }

            _iconImage.sprite = _partConfig.IconSprite;
            _iconImage.SetNativeSize();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Selected?.Invoke(_partConfig);
        }
    }
}