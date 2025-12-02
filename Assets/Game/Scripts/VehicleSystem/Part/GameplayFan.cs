using UnityEngine;

namespace VehicleSystem.Part
{
    public class GameplayFan : GameplayPart
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _force;

        private bool _isAccelerate;

        private void Update()
        {
            _isAccelerate = Input.GetMouseButton(0);
        }

        private void FixedUpdate()
        {
            if (_isAccelerate)
            {
                _rigidbody.AddForce(transform.forward * _force, ForceMode.Acceleration);
            }
        }
    }
}