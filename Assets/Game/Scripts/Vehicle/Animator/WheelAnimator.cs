using UnityEngine;

namespace Vehicle.Animator
{
    public class WheelAnimator : MonoBehaviour
    {
        [SerializeField] private WheelCollider _wheelCollider;
        [SerializeField] private Transform _transform;

        private Quaternion _rotationOffset;

        private void Awake()
        {
            _rotationOffset = _transform.rotation;
        }

        private void FixedUpdate()
        {
            if (_wheelCollider == null || _transform == null)
            {
                return;
            }

            _wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

            _transform.position = new Vector3(_transform.position.x, position.y, _transform.position.z);
            _transform.rotation = rotation * _rotationOffset;
        }
    }
}