using InputSystem;
using UnityEngine;
using Zenject;

namespace VehicleSystem.Parts.Gameplay
{
    public class GameplayPartBooster : GameplayAttachablePart
    {
        [SerializeField] private float _force;

        private IInputHandler _inputHandler;
        private Rigidbody _rigidbody;

        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        private void Awake()
        {
            _rigidbody = GetComponentInParent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_inputHandler.IsAccelerating)
            {
                _rigidbody.AddForceAtPosition(transform.forward * _force, transform.position, ForceMode.Acceleration);
            }
        }
    }
}