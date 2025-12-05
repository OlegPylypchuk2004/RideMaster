using UI.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScene
{
    public class MenuSceneUIManager : MonoBehaviour
    {
        [SerializeField] private Button _settingButton;
        [SerializeField] private Popup _settingsPopup;

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