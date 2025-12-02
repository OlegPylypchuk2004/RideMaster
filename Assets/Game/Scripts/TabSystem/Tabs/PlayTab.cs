using LocationSystem;
using UI.ScreenSystem;
using UnityEngine;

namespace TabSystem.Tabs
{
    public class PlayTab : Tab
    {
        [SerializeField] private LocationButton[] _locationButtons;
        [SerializeField] private UIScreen _locationsScreen;
        [SerializeField] private UIScreen _levelsScreen;

        public override void Activate()
        {
            base.Activate();

            foreach (LocationButton locationButton in _locationButtons)
            {
                locationButton.Selected += OnLocationButtonSelected;
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();

            foreach (LocationButton locationButton in _locationButtons)
            {
                locationButton.Selected -= OnLocationButtonSelected;
            }
        }

        private void OnLocationButtonSelected(LocationConfig locationConfig)
        {
            _locationsScreen.Disappear(() =>
            {
                _levelsScreen.Appear();
            });
        }
    }
}