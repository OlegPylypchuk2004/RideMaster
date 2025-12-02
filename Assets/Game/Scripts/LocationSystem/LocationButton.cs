using System;
using UnityEngine;
using UnityEngine.UI;

namespace LocationSystem
{
    public class LocationButton : MonoBehaviour
    {
        [SerializeField] private LocationConfig _locationConfig;
        [SerializeField] private Button _button;

        public event Action<LocationConfig> Selected;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Selected?.Invoke(_locationConfig);
        }
    }
}