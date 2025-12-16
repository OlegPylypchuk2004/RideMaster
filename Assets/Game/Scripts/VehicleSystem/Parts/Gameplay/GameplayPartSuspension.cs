using InputSystem;
using UnityEngine;
using Zenject;

namespace VehicleSystem.Parts.Gameplay
{
    public class GameplayPartSuspension : GameplayAttachablePart
    {
        [SerializeField] private Wheel[] _wheels;

        private IInputHandler _inputHandler;

        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        private void Update()
        {
            foreach (Wheel wheel in _wheels)
            {
                wheel.IsAppliedBrakeTorque = _inputHandler.IsBraking;
            }
        }
    }
}