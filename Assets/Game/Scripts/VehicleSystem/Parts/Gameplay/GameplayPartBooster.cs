using UnityEngine;

namespace VehicleSystem.Parts.Gameplay
{
    public class GameplayPartBooster : GameplayPart
    {
        [SerializeField] private float _force;

        private Rigidbody _rigidbody;
        private bool _isAccelerate;

        public bool IsActive => _isAccelerate;

        private void Awake()
        {
            _rigidbody = GetComponentInParent<Rigidbody>();
        }

        private void Update()
        {
            _isAccelerate = Input.GetKey(KeyCode.Space);
        }

        private void FixedUpdate()
        {
            if (_isAccelerate)
            {
                _rigidbody.AddForce(_rigidbody.transform.forward * _force, ForceMode.Acceleration);
            }
        }
    }
}