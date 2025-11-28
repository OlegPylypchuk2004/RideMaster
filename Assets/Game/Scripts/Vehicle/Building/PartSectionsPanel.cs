using UnityEngine;

namespace Vehicle.Building
{
    public class PartSectionsPanel : MonoBehaviour
    {
        [SerializeField] private PartSection _partSectionPrefab;
        [SerializeField] private WorldGridLayoutGroup _worldGridLayoutGroup;

        private PartSection[,] _partSections;

        public PartSection[,] PartSections => _partSections;

        public void Initialize(PartsGridConfig partsGridConfig)
        {
            _partSections = new PartSection[partsGridConfig.Size.rows, partsGridConfig.Size.columns];

            for (int rowIndex = 0; rowIndex < _partSections.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _partSections.GetLength(1); columnIndex++)
                {
                    _partSections[rowIndex, columnIndex] = Instantiate(_partSectionPrefab, transform);
                }
            }

            _worldGridLayoutGroup.Columns = partsGridConfig.Size.columns;
            _worldGridLayoutGroup.UpdateLayout();
        }
    }
}