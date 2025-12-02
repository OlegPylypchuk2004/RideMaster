using LocationSystem;
using SessionSystem;
using UnityEngine;
using Zenject;

namespace UI.ScreenSystem.Screens
{
    public class LocationsUIScreen : UIScreen
    {
        [SerializeField] private LocationButton[] _locationButtons;
        [SerializeField] private LevelsUIScreen _levelsUIScreen;

        private SessionData _sessionData;

        [Inject]
        private void Construct(SessionData sessionData)
        {
            _sessionData = sessionData;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            foreach (LocationButton locationButton in _locationButtons)
            {
                locationButton.Selected += OnLocationButtonSelected;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            foreach (LocationButton locationButton in _locationButtons)
            {
                locationButton.Selected -= OnLocationButtonSelected;
            }
        }

        private void OnLocationButtonSelected(LocationConfig locationConfig)
        {
            if (locationConfig == null)
            {
                return;
            }

            _sessionData.locationConfig = locationConfig;

            Disappear(() =>
            {
                _levelsUIScreen.Appear();
            });
        }
    }
}