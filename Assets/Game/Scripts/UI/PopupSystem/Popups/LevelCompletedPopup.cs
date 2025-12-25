using LevelSystem;
using SceneLoadingSystem;
using SessionSystem;
using UnityEngine;
using UnityEngine.UI;
using VehicleSystem;
using Zenject;

namespace UI.PopupSystem.Popups
{
    public class LevelCompletedPopup : Popup
    {
        [SerializeField] private Button _continueButton;

        private SceneLoader _sceneLoader;
        private Vehicle _vehicle;

        [Inject]
        private void Construct(SceneLoader sceneLoader, Vehicle vehicle, SessionData sessionData)
        {
            _sceneLoader = sceneLoader;
            _vehicle = vehicle;
        }

        protected override void Start()
        {
            base.Start();

            StarsCountCalculator starsCountCalculator = new StarsCountCalculator();
            int starsCount = starsCountCalculator.Calculate(_vehicle);

            Debug.LogError(starsCount);
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