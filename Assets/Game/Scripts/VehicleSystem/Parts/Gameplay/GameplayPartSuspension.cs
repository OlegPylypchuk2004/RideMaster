using UnityEngine;

namespace VehicleSystem.Parts.Gameplay
{
    public class GameplayPartSuspension : GameplayPart
    {
        [SerializeField] private WheelCollider[] _wheelColliders;
        [SerializeField] private float _brakeTorque;

        private bool _isBraking;

        private void Update()
        {
            _isBraking = !Input.GetKey(KeyCode.Space);
        }

        private void FixedUpdate()
        {
            float currentBrakeTorque = 0f;

            if (_isBraking)
            {
                currentBrakeTorque = _brakeTorque;
            }

            foreach (WheelCollider wheelCollider in _wheelColliders)
            {
                wheelCollider.brakeTorque = currentBrakeTorque;
            }
        }
    }
}