using UnityEngine;

namespace TabSystem
{
    public class TabChanger : MonoBehaviour
    {
        [SerializeField] private TabButton[] _tabButtons;
        [SerializeField] private Tab _initialTab;

        private Tab _activeTab;

        private void Awake()
        {
            _activeTab = _initialTab;
            _activeTab.Activate();
        }

        private void OnEnable()
        {
            foreach (TabButton tabButton in _tabButtons)
            {
                tabButton.Selected += OnTabButtonSelected;
            }
        }

        private void OnDisable()
        {
            foreach (TabButton tabButton in _tabButtons)
            {
                tabButton.Selected -= OnTabButtonSelected;
            }
        }

        private void OnTabButtonSelected(Tab tab)
        {
            if (tab == null)
            {
                return;
            }

            if (_activeTab != null)
            {
                if (_activeTab == tab)
                {
                    return;
                }

                _activeTab.Deactivate();
            }

            _activeTab = tab;
            _activeTab.Activate();
        }
    }
}