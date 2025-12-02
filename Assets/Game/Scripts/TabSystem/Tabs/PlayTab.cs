using LocationSystem;
using UI.ScreenSystem.Screens;
using UnityEngine;

namespace TabSystem.Tabs
{
    public class PlayTab : Tab
    {
        [SerializeField] private LocationsUIScreen _locationsUIScreen;
        [SerializeField] private LevelsUIScreen _levelsUIScreen;

        public override void Activate()
        {
            base.Activate();

            _locationsUIScreen.LocationSelected += OnLocationSelected;

            _levelsUIScreen.BackButtonClicked += OnLevelsUIScreenBackButtonClicked;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            _locationsUIScreen.LocationSelected -= OnLocationSelected;

            _levelsUIScreen.BackButtonClicked -= OnLevelsUIScreenBackButtonClicked;
        }

        private void OnLocationSelected(LocationConfig locationConfig)
        {
            _locationsUIScreen.Disappear(() =>
            {
                _levelsUIScreen.Appear();
            });
        }

        private void OnLevelsUIScreenBackButtonClicked()
        {
            _levelsUIScreen.Disappear(() =>
            {
                _locationsUIScreen.Appear();
            });
        }
    }
}