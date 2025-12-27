using UnityEngine;
using Zenject;

namespace VehicleSystem
{
    public class VehicleStopChecker : ITickable
    {
        private readonly float _minVehicleLinearVelocity;
        private readonly Vehicle _vehicle;

        private float _currentTime;

        public VehicleStopChecker(Vehicle vehicle, float minVehicleLinearVelocity)
        {
            _vehicle = vehicle;
            _minVehicleLinearVelocity = minVehicleLinearVelocity;
        }

        public bool IsActive { get; set; }

        public void Tick()
        {
            if (!IsActive)
            {
                return;
            }

            if (_vehicle.LinearVelocity.magnitude < _minVehicleLinearVelocity)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                _currentTime = 0f;
            }
        }
    }
}