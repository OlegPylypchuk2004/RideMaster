using UnityEngine;
using VehicleSystem.Parts;
using VehicleSystem.Parts.Gameplay;
using Zenject;

namespace VehicleSystem.Building
{
    public class VehicleBuilder : MonoBehaviour
    {
        [SerializeField] private PartSectionsPanel _partSectionsPanel;
        [SerializeField] private PartButtonsPanel _partButtonsPanel;
        [SerializeField] private Camera _camera;
        [SerializeField] private PreviewPartHolder _previewPartHolder;
        [SerializeField] private Vector3 _mouseOffset;

        private PartsGridData _partsGridData;
        private PartConfig _selectedPartConfig;

        [Inject]
        private void Construct(PartsGridData partsGridData)
        {
            _partsGridData = partsGridData;
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
            if (_previewPartHolder.Part != null)
            {
                return false;
            }

            foreach (PartButton partButton in _partButtonsPanel.PartButtons)
            {
                PartData partData = partButton.PartData;

                if (partData == null)
                {
                    continue;
                }

                if (partData.Config == null)
                {
                    continue;
                }

                if (partData.Config.GameplayPrefab is GameplayBasePart && partData.Count > 0)
                {
                    return false;
                }
            }

            return true;
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
                _mouseOffset.x,
                worldMousePosition.y + _mouseOffset.y,
                worldMousePosition.z + _mouseOffset.z
            );
        }

        private void HandlePreviewPlacement()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            ray.origin += _mouseOffset;

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
                //partSection.UpdatePartRotation();
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