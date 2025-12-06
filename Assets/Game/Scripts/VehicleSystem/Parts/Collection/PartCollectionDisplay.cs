using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace VehicleSystem.Parts.Collection
{
    public class PartCollectionDisplay : MonoBehaviour
    {
        [SerializeField] private PartConfig _partConfig;
        [SerializeField] private Image _imageIcon;
        [SerializeField] private TMP_Text _text;

        private SaveManager _saveManager;

        [Inject]
        private void Construct(SaveManager saveManager)
        {
            _saveManager = saveManager;
        }

        private void OnEnable()
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (_partConfig == null)
            {
                Debug.LogError("Part config not found");

                return;
            }

            if (_saveManager.Data.availablePartsCollection.Contains(_partConfig))
            {
                _imageIcon.sprite = _partConfig.IconSprite;
                _text.gameObject.SetActive(false);
            }
            else
            {
                _imageIcon.sprite = _partConfig.LockedIconSprite;
                _text.gameObject.SetActive(true);
            }

            _imageIcon.SetNativeSize();
        }
    }
}