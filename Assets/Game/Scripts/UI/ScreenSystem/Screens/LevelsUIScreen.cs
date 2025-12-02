using LevelSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenSystem.Screens
{
    public class LevelsUIScreen : UIScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private LocationsUIScreen _locationsUIScreen;
        [SerializeField] private LevelButton _levelButtonPrefab;

        private HashSet<LevelButton> _levelButtons;

        protected override void Awake()
        {
            base.Awake();

            _levelButtons = new HashSet<LevelButton>();
        }

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
            Disappear(() =>
            {
                _locationsUIScreen.Appear();
            });
        }
    }
}