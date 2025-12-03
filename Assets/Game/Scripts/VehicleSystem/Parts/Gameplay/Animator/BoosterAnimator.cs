using UnityEngine;

namespace VehicleSystem.Parts.Gameplay.Animator
{
    public class BoosterAnimator : MonoBehaviour
    {
        [SerializeField] private float _maxRotationSpeed;
        [SerializeField, Min(0f)] private float _rotationLerpSpeed;
        [SerializeField] private Transform _rotationTransform;
        [SerializeField] private GameplayPartBooster _booster;

        private float _currentRotationSpeed;

        private void Update()
        {
            float targetSpeed = 0f;

            if (_booster.IsActive)
            {
                targetSpeed = _maxRotationSpeed;
            }

            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, targetSpeed, Time.deltaTime * _rotationLerpSpeed);

            Quaternion rotationDelta = Quaternion.Euler(0f, 0f, _currentRotationSpeed * Time.deltaTime);
            _rotationTransform.localRotation *= rotationDelta;
        }
    }
}