using SaveSystem;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace PerkSystem
{
    public class PerkButton : MonoBehaviour
    {
        [SerializeField] private PerkConfig _perkConfig;
        [SerializeField] private GameObject _availableDisplay;
        [SerializeField] private GameObject _notAvailableDisplay;
        [SerializeField] private TMP_Text _nameTextMesh;
        [SerializeField] private TMP_Text _valueTextMesh;

        private SaveManager _saveManager;

        [Inject]
        private void Construct(SaveManager saveManager)
        {
            _saveManager = saveManager;
        }

        private void Start()
        {
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_perkConfig != null)
            {
                PerkData perkData = GetPerkData();

                if (perkData != null)
                {
                    _availableDisplay.SetActive(true);
                    _notAvailableDisplay.SetActive(false);

                    _nameTextMesh.text = _perkConfig.DisplayName;
                    _valueTextMesh.text = $"+{perkData.value}%";
                }
                else
                {
                    _availableDisplay.SetActive(false);
                    _notAvailableDisplay.SetActive(true);
                }
            }
            else
            {
                _availableDisplay.SetActive(false);
                _notAvailableDisplay.SetActive(true);
            }
        }

        private PerkData GetPerkData()
        {
            return _saveManager.Data.availablePerks.FirstOrDefault(perkData => perkData.id == _perkConfig.ID);
        }
    }
}