using System;
using UnityEngine;
using UnityEngine.UI;

namespace TabSystem
{
    public class TabButton : MonoBehaviour
    {
        [SerializeField] private Tab _tab;
        [SerializeField] private Button _button;

        public Tab Tab => _tab;

        public event Action<TabButton> Selected;

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
            Selected?.Invoke(this);
        }
    }
}