using UnityEngine;
using VehicleSystem;
using VehicleSystem.Parts;
using VehicleSystem.Parts.Gameplay;
using Zenject;

namespace CameraManagment
{
    public class VehicleFollowCamera : MonoBehaviour
    {
        private Transform _targetTransform;
        private Vector3 _offset;

        [Inject]
        private void Construct(Vehicle vehicle)
        {
            foreach (GameplayPart part in vehicle.Parts)
            {
                if (part is GameplayPartMain)
                {
                    _targetTransform = part.transform;

                    return;
                }
            }
        }

        private void Awake()
        {
            if (_targetTransform != null)
            {
                _offset = transform.position - _targetTransform.position;
            }
        }

        private void LateUpdate()
        {
            if (_targetTransform != null)
            {
                transform.position = _targetTransform.position + _offset;
            }
        }
    }
}