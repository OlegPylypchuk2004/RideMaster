using SessionSystem;
using UnityEngine;
using UnityEngine.UIElements;
using VehicleSystem.Building;
using VehicleSystem.Parts;
using Zenject;

namespace WorkshopScene
{
    public class WorkshopSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private VehicleBuilder _vehicleBuilder;
        [SerializeField] private PartSectionsPanel _partSectionsPanel;
        [SerializeField] private PartButtonsPanel _partButtonsPanel;

        private PartsGridData _partsGridData;
        private PartsGridConfig _partsGridConfig;

        [Inject]
        private void Construct(PartsGridData partsGridData, SessionData sessionData)
        {
            _partsGridData = partsGridData;
            _partsGridConfig = sessionData.levelConfig.PartsGridConfig;
        }

        private void Start()
        {
            _partSectionsPanel.Initialize(_partsGridConfig);

            int rows = _partsGridConfig.Size.rows;
            int columns = _partsGridConfig.Size.columns;

            if (_partsGridData.partConfigs == null ||
                _partsGridData.partConfigs.GetLength(0) != rows ||
                _partsGridData.partConfigs.GetLength(1) != columns)
            {
                _partsGridData.partConfigs = new PartConfig[rows, columns];

                return;
            }

            RestorePartSections();
        }

        private void RestorePartSections()
        {
            int rows = _partsGridConfig.Size.rows;
            int columns = _partsGridConfig.Size.columns;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    PartConfig partConfig = _partsGridData.partConfigs[row, column];

                    if (partConfig != null)
                    {
                        PartSection partSection = _partSectionsPanel.PartSections[row, column];
                        partSection.TrySetPart(partConfig);
                    }
                }
            }
        }
    }
}