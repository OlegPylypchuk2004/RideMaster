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
        [SerializeField] private PartSection[] _partSections;

        private PartConfig _partConfig;
        private PartPreview _partPreview;

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
            foreach (PartSection partSection in _partSections)
            {
                if (partSection.IsEmpty())
                {
                    continue;
                }

                Transform buildPoint = partSection.BuildPoint;
                Instantiate(partSection.PartConfig.GameplayPrefab, buildPoint);
            }
        }
    }
}