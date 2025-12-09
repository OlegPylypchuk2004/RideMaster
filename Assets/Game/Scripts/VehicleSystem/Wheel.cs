using UnityEngine;

namespace VehicleSystem
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _radius;
        [SerializeField, Min(0f)] private float _suspensionDistance;
        [SerializeField, Min(0f)] private float _springStrength;
        [SerializeField, Min(0f)] private float _springDamper;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _visualTransform;

        public bool IsGrounded { get; private set; }
        public float Compression { get; private set; }

        private Rigidbody _rigidbody;
        private float _previousCompression;
        private float _wheelRotationAngle;
        private Vector3 _hitPoint;
        private Vector3 _initialLocalPosition;

        private void Awake()
        {
            _rigidbody = GetComponentInParent<Rigidbody>();

            if (_visualTransform != null)
            {
                _initialLocalPosition = _visualTransform.localPosition;
            }
        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);

            DetectGround();
            ApplySuspension();
            UpdateWheelRotation();
            UpdateWheelVisual();
        }

        private void DetectGround()
        {
            Vector3 rayOrigin = transform.position + (transform.up * _suspensionDistance);
            Vector3 direction = -transform.up;

            float maxRayLength = _suspensionDistance + _radius;

            if (Physics.Raycast(rayOrigin, direction, out RaycastHit hit, maxRayLength, _groundLayer))
            {
                IsGrounded = true;
                _hitPoint = hit.point;

                float currentDistance = hit.distance;
                float compressionLength = maxRayLength - currentDistance;

                Compression = Mathf.Clamp01((_suspensionDistance - (currentDistance - _radius)) / _suspensionDistance);
            }
            else
            {
                IsGrounded = false;
                Compression = 0f;
            }
        }

        private void ApplySuspension()
        {
            if (!IsGrounded || _rigidbody == null)
            {
                return;
            }

            float springForce = _springStrength * Compression;
            float damperForce = _springDamper * (Compression - _previousCompression) / Time.fixedDeltaTime;
            float totalForce = springForce + damperForce;
            Vector3 forceVector = transform.up * totalForce;

            _rigidbody.AddForceAtPosition(forceVector, transform.position, ForceMode.Force);
            _previousCompression = Compression;
        }

        private void UpdateWheelRotation()
        {
            if (_rigidbody == null)
            {
                return;
            }

            Vector3 velocity = _rigidbody.GetPointVelocity(transform.position);
            float forwardSpeed = Vector3.Dot(velocity, transform.forward);
            float deltaRotation = (forwardSpeed / _radius) * Time.fixedDeltaTime;
            _wheelRotationAngle += deltaRotation;
        }

        private void UpdateWheelVisual()
        {
            if (_visualTransform == null)
            {
                return;
            }

            float visualOffset = _suspensionDistance * Compression;
            Vector3 newPosition = _initialLocalPosition;
            newPosition.y += visualOffset;

            _visualTransform.localPosition = newPosition;
            _visualTransform.localRotation = Quaternion.Euler(_wheelRotationAngle * Mathf.Rad2Deg, 0f, 0f);
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 rayOrigin = transform.position + (transform.up * _suspensionDistance);
            Vector3 direction = -transform.up;
            float maxRayLength = _suspensionDistance + _radius;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(rayOrigin, rayOrigin + (direction * maxRayLength));

            if (IsGrounded)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_hitPoint, 0.05f);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(rayOrigin, _hitPoint);

                Vector3 wheelVisualPos = transform.position + (transform.up * (_suspensionDistance * Compression));
                Gizmos.DrawWireSphere(wheelVisualPos, _radius);
            }
            else
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, _radius);
            }
        }
    }
}