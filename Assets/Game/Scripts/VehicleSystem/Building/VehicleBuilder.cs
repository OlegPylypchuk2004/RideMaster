using SessionSystem;
using UnityEngine;
using VehicleSystem.Parts;
using VehicleSystem.Parts.Preview;
using Zenject;

namespace VehicleSystem.Building
{
    public class VehicleBuilder : MonoBehaviour
    {
        [SerializeField] private PartSectionsPanel _partSectionsPanel;
        [SerializeField] private PartButtonsPanel _partButtonsPanel;
        [SerializeField] private Camera _camera;

        private PartsGridData _partsGridData;
        private PartsGridConfig _partsGridConfig;

        private PartConfig _selectedPartConfig;
        private PreviewPart _activePreviewPart;

        [Inject]
        private void Construct(PartsGridData partsGridData, SessionData sessionData)
        {
            _partsGridData = partsGridData;
            _partsGridConfig = sessionData.levelConfig.PartsGridConfig;
        }

        private void Awake()
        {
            _partsGridData.partConfigs = new PartConfig[_partsGridConfig.Size.rows, _partsGridConfig.Size.columns];

            _partSectionsPanel.Initialize(_partsGridConfig);
        }

        private void OnEnable()
        {
            _partButtonsPanel.PartConfigSelected += OnPartConfigSelected;
            _partSectionsPanel.PartConfigSelected += OnPartConfigSelected;
        }

        private void OnDisable()
        {
            _partButtonsPanel.PartConfigSelected -= OnPartConfigSelected;
            _partSectionsPanel.PartConfigSelected -= OnPartConfigSelected;
        }

        private void Update()
        {
            if (_activePreviewPart == null)
            {
                return;
            }

            HandlePreviewMovement();
            HandlePreviewPlacement();
        }

        private void HandlePreviewMovement()
        {
            if (!Input.GetMouseButton(0))
            {
                return;
            }

            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            _activePreviewPart.transform.position = new Vector3
            (
                0f,
                worldMousePosition.y,
                worldMousePosition.z
            );
        }

        private void HandlePreviewPlacement()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.collider.TryGetComponent(out PartSection partSection))
                {
                    bool successfullySet = partSection.TrySetPart(_selectedPartConfig);

                    if (!successfullySet)
                    {
                        _partButtonsPanel.ReturnPart(_selectedPartConfig);
                    }
                }
                else
                {
                    _partButtonsPanel.ReturnPart(_selectedPartConfig);
                }
            }
            else
            {
                _partButtonsPanel.ReturnPart(_selectedPartConfig);
            }

            DestroyActivePreviewPart();
        }

        public void BuildVehicle()
        {
            for (int rowIndex = 0; rowIndex < _partsGridData.partConfigs.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _partsGridData.partConfigs.GetLength(1); columnIndex++)
                {
                    _partsGridData.partConfigs[rowIndex, columnIndex] =
                        _partSectionsPanel.PartSections[rowIndex, columnIndex].PartConfig;
                }
            }
        }

        public void ResetVehicle()
        {
            for (int rowIndex = 0; rowIndex < _partsGridData.partConfigs.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _partsGridData.partConfigs.GetLength(1); columnIndex++)
                {
                    _partsGridData.partConfigs[rowIndex, columnIndex] = null;
                }
            }
        }

        private void OnPartConfigSelected(PartConfig partConfig)
        {
            if (_activePreviewPart != null)
            {
                return;
            }

            if (partConfig == null)
            {
                return;
            }

            if (partConfig.PreviewPrefab == null)
            {
                return;
            }

            _selectedPartConfig = partConfig;
            _activePreviewPart = Instantiate(partConfig.PreviewPrefab);
        }

        private void DestroyActivePreviewPart()
        {
            if (_activePreviewPart == null)
            {
                return;
            }

            Destroy(_activePreviewPart.gameObject);

            _selectedPartConfig = null;
            _activePreviewPart = null;
        }
    }
}