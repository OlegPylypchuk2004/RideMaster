using System;
using UnityEngine;
using VehicleSystem.Parts;
using WorldLayoutGroup;

namespace VehicleSystem.Building
{
    public class PartSectionsPanel : MonoBehaviour
    {
        [SerializeField] private PartSection _partSectionPrefab;
        [SerializeField] private WorldGridLayoutGroup _worldGridLayoutGroup;

        private PartSection[,] _partSections;

        public PartSection[,] PartSections => _partSections;

        public event Action<PartConfig> PartConfigSelected;

        private void OnDestroy()
        {
            foreach (PartSection partSection in _partSections)
            {
                partSection.Selected -= OnPartSectionSelected;
            }
        }

        public void Initialize(PartsGridConfig partsGridConfig)
        {
            _partSections = new PartSection[partsGridConfig.Size.rows, partsGridConfig.Size.columns];

            for (int rowIndex = 0; rowIndex < _partSections.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _partSections.GetLength(1); columnIndex++)
                {
                    PartSection partSection = Instantiate(_partSectionPrefab, transform);
                    partSection.Selected += OnPartSectionSelected;
                    partSection.name += $" [{rowIndex};{columnIndex}]";
                    _partSections[rowIndex, columnIndex] = partSection;
                }
            }

            _worldGridLayoutGroup.Columns = partsGridConfig.Size.columns;
            _worldGridLayoutGroup.UpdateLayout();
        }

        public void ResetSections()
        {
            foreach (PartSection partSection in _partSections)
            {
                partSection.TryRemovePart();
            }
        }

        private void OnPartSectionSelected(PartSection partSection)
        {
            if (partSection == null)
            {
                return;
            }

            if (partSection.PartConfig == null || partSection.PartPreview == null)
            {
                return;
            }

            PartConfigSelected?.Invoke(partSection.PartConfig);

            partSection.TryRemovePart();
        }
    }
}