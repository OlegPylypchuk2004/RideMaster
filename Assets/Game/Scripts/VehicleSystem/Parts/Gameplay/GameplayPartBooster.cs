using UnityEngine;

namespace VehicleSystem.Parts.Gameplay
{
    public class GameplayPartBooster : GameplayPart
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _force;

        private bool _isAccelerate;

        public bool IsActive => _isAccelerate;

        private void Update()
        {
            _isAccelerate = Input.GetKey(KeyCode.Space);
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