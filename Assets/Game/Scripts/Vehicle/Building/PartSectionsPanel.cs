using UnityEngine;

namespace Vehicle.Building
{
    public class PartSectionsPanel : MonoBehaviour
    {
        [SerializeField] private PartSection _partSectionPrefab;

        private PartSection[,] _partSections;

        public PartSection[,] PartSections => _partSections;

        public void Initialize(PartsGridConfig partsGridConfig)
        {
            _partSections = new PartSection[partsGridConfig.Size.rows, partsGridConfig.Size.columns];

            float offsetX = (partsGridConfig.Size.columns - 1) / 2f;
            float offsetY = (partsGridConfig.Size.rows - 1) / 2f;

            for (int rowIndex = 0; rowIndex < _partSections.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _partSections.GetLength(1); columnIndex++)
                {
                    PartSection partSection = Instantiate(_partSectionPrefab, transform);
                    partSection.transform.localPosition = new Vector3(columnIndex - offsetX, rowIndex - offsetY, 0f);

                    _partSections[rowIndex, columnIndex] = partSection;
                }
            }
        }
    }
}