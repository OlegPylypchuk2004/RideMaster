using UnityEngine;
using Vehicle.Part;
using Vehicle.Part.Configs;
using Vehicle.Part.UI;

namespace Vehicle.Building
{
    public class VehicleBuilder : MonoBehaviour
    {
        [SerializeField] private PartButton[] _partButtons;
        [SerializeField] private Camera _camera;
        [SerializeField] private PartsGridConfig _partsGridConfig;
        [SerializeField] private PartSection _partSectionPrefab;

        private PartSection[,] _partSections;
        private PartConfig _partConfig;
        private PreviewPart _partPreview;

        private void Awake()
        {
            _partSections = new PartSection[_partsGridConfig.Size.rows, _partsGridConfig.Size.columns];

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

        private void OnEnable()
        {
            foreach (PartButton partButton in _partButtons)
            {
                partButton.Selected += OnPartButtonSelected;
            }
        }

        private void OnDisable()
        {
            foreach (PartButton partButton in _partButtons)
            {
                partButton.Selected -= OnPartButtonSelected;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Build();
            }

            if (_partPreview == null)
            {

            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 worldMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                    Vector3 partPreviewTargetPosition = worldMousePosition;
                    partPreviewTargetPosition.z = 0f;

                    _partPreview.transform.position = partPreviewTargetPosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit raycastHit))
                    {
                        if (raycastHit.collider.TryGetComponent(out PartSection partSection))
                        {
                            if (partSection.TrySetPart(_partConfig))
                            {

                            }
                        }
                    }

                    Destroy(_partPreview.gameObject);
                    _partConfig = null;
                    _partPreview = null;
                }
            }
        }

        private void OnPartButtonSelected(PartConfig partConfig)
        {
            if (partConfig == null || partConfig.PreviewPrefab == null)
            {
                return;
            }

            _partConfig = partConfig;
            _partPreview = Instantiate(partConfig.PreviewPrefab);
        }

        private void Build()
        {
            for (int rowIndex = 0; rowIndex < _partSections.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _partSections.GetLength(1); columnIndex++)
                {
                    PartConfig partConfig = _partSections[rowIndex, columnIndex].PartConfig;

                    if (partConfig == null)
                    {
                        continue;
                    }

                    GameplayPart gameplayPart = Instantiate(partConfig.GameplayPrefab);
                    gameplayPart.transform.position = new Vector3(columnIndex, rowIndex, 0f) + Vector3.right * 5f;
                }
            }
        }
    }
}