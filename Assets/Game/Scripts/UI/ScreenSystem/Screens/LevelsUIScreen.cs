using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenSystem.Screens
{
    public class LevelsUIScreen : UIScreen
    {
        [SerializeField] private Button _backButton;

        public event Action BackButtonClicked;

        protected override void OnEnable()
        {
            base.OnEnable();

            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            BackButtonClicked?.Invoke();
        }
    }
}