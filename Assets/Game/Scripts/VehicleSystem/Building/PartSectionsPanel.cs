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

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    PartSection section = Instantiate(_partSectionPrefab, transform);

                    section.Selected += OnPartSectionSelected;

                    section.name += $" [{r};{c}]";

                    _partSections[r, c] = section;
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