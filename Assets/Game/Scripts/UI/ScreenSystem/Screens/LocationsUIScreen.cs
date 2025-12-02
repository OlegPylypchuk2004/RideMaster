using LocationSystem;
using System;
using UnityEngine;

namespace UI.ScreenSystem.Screens
{
    public class LocationsUIScreen : UIScreen
    {
        [SerializeField] private LocationButton[] _locationButtons;

        public event Action<LocationConfig> LocationSelected;

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
            LocationSelected?.Invoke(locationConfig);
        }
    }
}