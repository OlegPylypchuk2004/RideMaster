using UnityEngine;

namespace VehicleSystem
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _radius;
        [SerializeField, Min(0f)] private float _suspensionDistance;
        [SerializeField, Min(0f)] private float _springStrength;
        [SerializeField, Min(0f)] private float _springDamper;
        [SerializeField, Min(0f)] private float _longitudinalFrictionForce;
        [SerializeField, Min(0f)] private float _lateralFrictionForce;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _visualTransform;

        public bool IsGrounded { get; private set; }
        public float Compression { get; private set; }
        public float AppliedDriveTorque { get; set; }

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
            DetectGround();
            ApplySuspension();
            ApplyWheelForces();
            UpdateWheelRotation();
            UpdateWheelVisual();
        }

        private void DetectGround()
        {
            Vector3 suspensionUp = Vector3.up;

            Vector3 rayOrigin = transform.position + suspensionUp * _suspensionDistance;
            Vector3 direction = -suspensionUp;

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

            Vector3 suspensionUp = Vector3.up;

            float springForce = _springStrength * Compression;
            float damperForce = _springDamper * (Compression - _previousCompression) / Time.fixedDeltaTime;
            float totalForce = springForce + damperForce;

            Vector3 forceVector = suspensionUp * totalForce;

            _rigidbody.AddForceAtPosition(forceVector, transform.position, ForceMode.Force);

            _previousCompression = Compression;
        }

        private void ApplyWheelForces()
        {
            if (!IsGrounded || _rigidbody == null)
            {
                return;
            }

            Vector3 wheelVelocity = _rigidbody.GetPointVelocity(transform.position);

            Vector3 lateralDirection = transform.right;
            float lateralSpeed = Vector3.Dot(wheelVelocity, lateralDirection);
            Vector3 lateralForce = -lateralDirection * lateralSpeed * _lateralFrictionForce;
            _rigidbody.AddForceAtPosition(lateralForce, transform.position, ForceMode.Force);

            Vector3 longitudinalDirection = transform.forward;
            float longitudinalSpeed = Vector3.Dot(wheelVelocity, longitudinalDirection);
            Vector3 longitudinalFriction = -longitudinalDirection * longitudinalSpeed * _longitudinalFrictionForce;
            _rigidbody.AddForceAtPosition(longitudinalFriction, transform.position, ForceMode.Force);

            if (AppliedDriveTorque != 0f)
            {
                float tractionForceValue = AppliedDriveTorque / _radius;
                Vector3 tractionForce = longitudinalDirection * tractionForceValue;
                _rigidbody.AddForceAtPosition(tractionForce, transform.position, ForceMode.Force);
            }
        }

        private void UpdateWheelRotation()
        {
            if (_rigidbody == null)
            {
                return;
            }

            Vector3 velocity = _rigidbody.GetPointVelocity(transform.position);

            if (!IsGrounded)
            {
                velocity = Vector3.zero;
            }

            float forwardSpeed = Vector3.Dot(velocity, transform.forward);
            float deltaRotation = forwardSpeed / _radius * Time.fixedDeltaTime;
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
            newPosition.y = _initialLocalPosition.y + visualOffset;

            _visualTransform.localPosition = newPosition;
            _visualTransform.localRotation = Quaternion.Euler(_wheelRotationAngle * Mathf.Rad2Deg, 0f, 0f);
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 suspensionUp = Vector3.up;

            Vector3 rayOrigin = transform.position + suspensionUp * _suspensionDistance;
            Vector3 direction = -suspensionUp;
            float maxRayLength = _suspensionDistance + _radius;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(rayOrigin, rayOrigin + direction * maxRayLength);

            if (IsGrounded)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_hitPoint, 0.05f);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(rayOrigin, _hitPoint);

                Vector3 wheelVisualPos = transform.position + suspensionUp * (_suspensionDistance * Compression);
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
