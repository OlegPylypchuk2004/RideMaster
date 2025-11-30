using UnityEngine;

namespace CameraManagment
{
    public class FollowCamera : MonoBehaviour
    {
        private Transform _targetTransform;
        private Vector3 _offset;

        private void LateUpdate()
        {
            if (_targetTransform != null)
            {
                transform.position = _targetTransform.position + _offset;
            }
        }

        public void SetTarget(Transform targetTransform)
        {
            if (targetTransform == null)
            {
                return;
            }

            _targetTransform = targetTransform;
            _offset = transform.position - _targetTransform.position;
        }
    }
}