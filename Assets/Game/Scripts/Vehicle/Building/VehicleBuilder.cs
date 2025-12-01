using UnityEngine;
using UnityEngine.SceneManagement;
using Vehicle.Part;
using Zenject;

namespace Vehicle.Building
{
    public class VehicleBuilder : MonoBehaviour
    {
        [SerializeField] PartSectionsPanel _partSectionPanel;
        [SerializeField] private PartButtonsPanel _partButtonsPanel;
        [SerializeField] private Camera _camera;
        [SerializeField] private PartsGridConfig _partsGridConfig;

        private PartsGridData _partsGridData;
        private PartConfig _partConfig;
        private PreviewPart _partPreview;

        [Inject]
        private void Construct(PartsGridData partsGridData)
        {
            _partsGridData = partsGridData;
        }

        private void Awake()
        {
            _partsGridData.partConfigs = new PartConfig[_partsGridConfig.Size.rows, _partsGridConfig.Size.columns];
            _partSectionPanel.Initialize(_partsGridConfig);
        }

        private void OnEnable()
        {
            _partButtonsPanel.PartConfigSelected += OnPartConfigSelected;
        }

        private void OnDisable()
        {
            _partButtonsPanel.PartConfigSelected -= OnPartConfigSelected;
        }

        private void Update()
        {
            if (_partPreview == null)
            {

            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 worldMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                    _partPreview.transform.position = new Vector3(0f, worldMousePosition.y, worldMousePosition.z);
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

        private void OnPartConfigSelected(PartConfig partConfig)
        {
            if (partConfig == null || partConfig.PreviewPrefab == null)
            {
                return;
            }

            _partConfig = partConfig;
            _partPreview = Instantiate(partConfig.PreviewPrefab);
        }

        public void Build()
        {
            for (int rowIndex = 0; rowIndex < _partsGridData.partConfigs.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _partsGridData.partConfigs.GetLength(1); columnIndex++)
                {
                    _partsGridData.partConfigs[rowIndex, columnIndex] = _partSectionPanel.PartSections[rowIndex, columnIndex].PartConfig;
                }
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}