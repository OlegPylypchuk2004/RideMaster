using UnityEngine;

namespace VehicleSystem
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField] Transform wheelVisual;
        [SerializeField] float wheelRadius = 0.5f;
        [SerializeField] float raycastExtraLength = 0.2f;
        [SerializeField] float springStrength = 30000f;
        [SerializeField] float springDamper = 4000f;
        [SerializeField] LayerMask groundLayer;

        public bool WheelIsGrounded { get; private set; }
        public float WheelCompression { get; private set; }

        Rigidbody parentRigidbody;
        float previousCompression;
        float wheelRotationAngle;

        void Awake()
        {
            parentRigidbody = GetComponentInParent<Rigidbody>();
        }

        void FixedUpdate()
        {
            UpdateGroundDetection();
            ApplySuspensionForce();
            UpdateWheelRotation();
            UpdateWheelVisual();
        }

        void UpdateGroundDetection()
        {
            Vector3 direction = -transform.up;
            Vector3 position = transform.position;

            float maxDistance = wheelRadius + raycastExtraLength;

            RaycastHit hitInfo;
            if (Physics.Raycast(position, direction, out hitInfo, maxDistance, groundLayer))
            {
                WheelIsGrounded = true;
                float distance = hitInfo.distance;
                WheelCompression = Mathf.Clamp01(1f - (distance - wheelRadius) / raycastExtraLength);
            }
            else
            {
                WheelIsGrounded = false;
                WheelCompression = 0f;
            }
        }

        void ApplySuspensionForce()
        {
            if (!WheelIsGrounded) return;

            float springForce = springStrength * WheelCompression;
            float damperForce = springDamper * (WheelCompression - previousCompression) / Time.fixedDeltaTime;

            float totalForce = springForce + damperForce;

            Vector3 force = transform.up * totalForce;
            Vector3 forcePosition = transform.position - transform.up * wheelRadius;
            parentRigidbody.AddForceAtPosition(force, forcePosition, ForceMode.Force);

            previousCompression = WheelCompression;
        }

        void UpdateWheelRotation()
        {
            if (parentRigidbody == null) return;

            Vector3 velocity = parentRigidbody.GetPointVelocity(transform.position);
            float forwardSpeed = Vector3.Dot(velocity, transform.forward);
            float deltaRotation = (forwardSpeed / wheelRadius) * Time.fixedDeltaTime;

            wheelRotationAngle += deltaRotation;
        }

        void UpdateWheelVisual()
        {
            if (wheelVisual == null) return;

            float offset = raycastExtraLength * (1f - WheelCompression);
            Vector3 localPos = wheelVisual.localPosition;
            localPos.y = -offset;
            wheelVisual.localPosition = localPos;

            wheelVisual.localRotation = Quaternion.Euler(wheelRotationAngle * Mathf.Rad2Deg, 0f, 0f);
        }

        void OnDrawGizmosSelected()
        {
            Vector3 position = transform.position;
            Vector3 direction = -transform.up;
            float maxDistance = wheelRadius + raycastExtraLength;

            if (WheelIsGrounded)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(position, position + direction * (wheelRadius + raycastExtraLength * (1f - WheelCompression)));
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(position, position + direction * maxDistance);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, wheelRadius);
        }
    }
}
