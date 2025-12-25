using InputSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace VehicleSystem.Parts.Gameplay.Animator
{
    public class BoosterAnimator : MonoBehaviour
    {
        [SerializeField] private float _maxRotationSpeed;
        [SerializeField, MinValue(0f)] private float _rotationLerpSpeed;
        [SerializeField] private Transform _rotationTransform;

        private IInputHandler _inputHandler;
        private float _currentRotationSpeed;

        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        private void Update()
        {
            float targetSpeed = 0f;

            if (_inputHandler.IsAccelerating)
            {
                targetSpeed = _maxRotationSpeed;
            }

            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, targetSpeed, Time.deltaTime * _rotationLerpSpeed);

            Quaternion rotationDelta = Quaternion.Euler(0f, 0f, _currentRotationSpeed * Time.deltaTime);
            _rotationTransform.localRotation *= rotationDelta;
        }
    }
}