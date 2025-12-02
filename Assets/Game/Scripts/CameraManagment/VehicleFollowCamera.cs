using System.Collections;
using UnityEngine;
using VehicleSystem;
using VehicleSystem.Parts;
using VehicleSystem.Parts.Gameplay;
using Zenject;

namespace CameraManagment
{
    public class VehicleFollowCamera : MonoBehaviour
    {
        private Vehicle _vehicle;
        private Transform _targetTransform;
        private Vector3 _offset;

        [Inject]
        private void Construct(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        private IEnumerator Start()
        {
            yield return new WaitWhile(() => _vehicle.Parts.Count == 0);

            foreach (GameplayPart part in _vehicle.Parts)
            {
                if (part is GameplayPartMain)
                {
                    _targetTransform = part.transform;
                    _offset = transform.position - _targetTransform.position;

                    yield break;
                }
            }

            _offset = transform.position - _targetTransform.position;
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