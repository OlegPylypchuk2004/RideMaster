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

        private Plane _partMovePlane;
        private PartPreview _partPreview;

        private void Awake()
        {
            _partMovePlane = new Plane(Vector3.forward, Vector3.zero);
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
            if (_partPreview == null)
            {

            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                    if (_partMovePlane.Raycast(ray, out float distance))
                    {
                        Vector3 hitPoint = ray.GetPoint(distance);
                        _partPreview.transform.position = hitPoint;
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Destroy(_partPreview.gameObject);
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

            _partPreview = Instantiate(partConfig.PreviewPrefab);
        }
    }
}