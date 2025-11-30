using NaughtyAttributes;
using UnityEngine;

namespace Vehicle.Animator
{
    public class WheelAnimator : MonoBehaviour
    {
        [SerializeField] private WheelCollider _wheelCollider;
        [SerializeField] private Transform _transform;
        [SerializeField] private bool _isUpdatePosition;
        [SerializeField, ShowIf("_isUpdatePosition")] private bool _isOffsetPosition;
        [SerializeField] private bool _isUpdateRotation;
        [SerializeField, ShowIf("_isUpdateRotation")] private bool _isOffsetRotation;

        private Vector3 _positionOffset;
        private Quaternion _rotationOffset;

        private void Awake()
        {
            if (_isOffsetPosition)
            {
                _positionOffset = _transform.position;
            }

            if (_isOffsetRotation)
            {
                _rotationOffset = _transform.rotation;
            }
        }

        private void FixedUpdate()
        {
            if (_wheelCollider == null || _transform == null)
            {
                return;
            }

            _wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

            if (_isUpdatePosition)
            {
                _transform.position = new Vector3(_transform.position.x, position.y, _transform.position.z) + _positionOffset;
            }

            if (_isOffsetRotation)
            {
                _transform.rotation = rotation * _rotationOffset;
            }
        }
    }
}