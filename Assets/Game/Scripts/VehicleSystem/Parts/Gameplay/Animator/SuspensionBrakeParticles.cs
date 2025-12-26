using Sirenix.OdinInspector;
using UnityEngine;

namespace VehicleSystem.Parts.Gameplay.Animator
{
    public class SuspensionBrakeParticles : MonoBehaviour
    {
        [SerializeField] private Wheel _wheel;
        [SerializeField, MinValue(0f)] private float _minVelocity;
        [SerializeField] private ParticleSystem _particleSystem;

        private void Update()
        {
            if (_particleSystem.isPlaying || _wheel.VehicleRigidbody == null)
            {
                return;
            }

            if (_wheel.IsGrounded && _wheel.IsAppliedBrakeTorque && _wheel.VehicleRigidbody.linearVelocity.z >= _minVelocity)
            {
                _particleSystem.Play();
            }
        }
    }
}