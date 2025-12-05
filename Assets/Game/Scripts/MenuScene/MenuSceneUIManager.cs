using UI.PopupSystem.Popups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MenuScene
{
    public class MenuSceneUIManager : MonoBehaviour
    {
        [SerializeField] private Button _settingButton;

        private SettingsPopup _settingsPopup;

        [Inject]
        private void Construct(SettingsPopup settingsPopup)
        {
            _settingsPopup = settingsPopup;
        }

        private void OnEnable()
        {
            _settingButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        private void OnDisable()
        {
            _settingButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }

        private void OnSettingsButtonClicked()
        {
            _settingsPopup.Appear();
        }
    }
}