using SessionSystem;
using UnityEngine;
using VehicleSystem.Parts;
using Zenject;

namespace VehicleSystem.Building
{
    public class VehicleBuilder : MonoBehaviour
    {
        [SerializeField] private PartSectionsPanel _partSectionsPanel;
        [SerializeField] private PartButtonsPanel _partButtonsPanel;
        [SerializeField] private Camera _camera;
        [SerializeField] private PreviewPartHolder _previewPartHolder;

        private PartsGridData _partsGridData;
        private PartsGridConfig _partsGridConfig;
        private PartConfig _selectedPartConfig;

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
            if (_previewPartHolder.Part == null)
            {
                return;
            }

            HandlePreviewMovement();
            HandlePreviewPlacement();
        }

        public bool IsCanBuildVehicle()
        {
            return _partButtonsPanel.IsAllButtonsAreEmpty();
        }

        public bool IsCanResetVehicle()
        {
            foreach (PartSection partSection in _partSectionsPanel.PartSections)
            {
                if (!partSection.IsEmpty())
                {
                    return true;
                }
            }

            return false;
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

        private void HandlePreviewMovement()
        {
            if (!Input.GetMouseButton(0))
            {
                return;
            }

            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            _previewPartHolder.transform.position = new Vector3
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

            foreach (PartSection partSection in _partSectionsPanel.PartSections)
            {
                partSection.UpdatePartRotation();
            }
        }

        private void OnPartConfigSelected(PartConfig partConfig)
        {
            if (_previewPartHolder.Part != null)
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
            _previewPartHolder.SetPart(Instantiate(partConfig.PreviewPrefab));
        }

        private void DestroyActivePreviewPart()
        {
            if (_previewPartHolder.Part == null)
            {
                return;
            }

            _selectedPartConfig = null;

            Destroy(_previewPartHolder.Part.gameObject);
            _previewPartHolder.ResetPart();
        }
    }
}