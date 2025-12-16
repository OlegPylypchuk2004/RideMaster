using SceneLoadingSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.PopupSystem.Popups
{
    public class LevelCompletedPopup : Popup
    {
        [SerializeField] private Button _continueButton;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }

        private void OnContinueButtonClicked()
        {
            _sceneLoader.Load(0);
        }
    }
}