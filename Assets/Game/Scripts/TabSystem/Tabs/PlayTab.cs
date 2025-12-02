using LocationSystem;
using UnityEngine;

namespace TabSystem.Tabs
{
    public class PlayTab : Tab
    {
        [SerializeField] private LocationButton[] _locationButtons;
        [SerializeField] private GameObject _mainSection;
        [SerializeField] private GameObject _levelsSection;

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
            _mainSection.SetActive(false);
            _levelsSection.SetActive(true);
        }
    }
}