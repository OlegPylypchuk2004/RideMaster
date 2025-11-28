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

            for (int rowIndex = 0; rowIndex < _partSections.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _partSections.GetLength(1); columnIndex++)
                {
                    PartSection partSection = Instantiate(_partSectionPrefab);
                    partSection.transform.position = new Vector3(columnIndex, rowIndex, 0f);

                    _partSections[rowIndex, columnIndex] = partSection;
                }
            }
        }
    }
}