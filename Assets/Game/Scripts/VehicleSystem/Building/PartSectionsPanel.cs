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

        public void Initialize(PartsGridConfig partsGridConfig)
        {
            int rows = partsGridConfig.Size.rows;
            int columns = partsGridConfig.Size.columns;

            _partSections = new PartSection[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    PartSection section = Instantiate(_partSectionPrefab, transform);
                    section.name += $" [{row};{column}]";
                    section.Selected += OnPartSectionSelected;

                    _partSections[row, column] = section;
                }
            }

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    PartSection topSection = (row > 0) ? _partSections[row - 1, column] : null;
                    PartSection bottomSection = (row < rows - 1) ? _partSections[row + 1, column] : null;
                    PartSection leftSection = (column > 0) ? _partSections[row, column - 1] : null;
                    PartSection rightSection = (column < columns - 1) ? _partSections[row, column + 1] : null;

                    _partSections[row, column].Initialize(topSection, bottomSection, rightSection, leftSection);
                }
            }

            _worldGridLayoutGroup.Columns = columns;
            _worldGridLayoutGroup.UpdateLayout();
        }

        public void ResetSections()
        {
            foreach (PartSection section in _partSections)
            {
                section.TryRemovePart();
            }
        }

        private void OnPartSectionSelected(PartSection partSection)
        {
            if (partSection == null)
            {
                return;
            }

            PartConfig partConfig = partSection.PartConfig;

            if (partConfig == null)
            {
                return;
            }

            if (partSection.PartPreview == null)
            {
                return;
            }

            PartConfigSelected?.Invoke(partConfig);

            partSection.TryRemovePart();
        }

        private void OnDestroy()
        {
            if (_partSections == null)
            {
                return;
            }

            foreach (PartSection section in _partSections)
            {
                section.Selected -= OnPartSectionSelected;
            }
        }
    }
}