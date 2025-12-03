using RoadSystem;
using UnityEngine;
using Zenject;

namespace CameraManagment
{
    public class VehicleFollowCamera : MonoBehaviour
    {
        private Road _road;
        private Vector3 _offset;

        [Inject]
        private void Construct(Road road)
        {
            _road = road;
        }

        private void Awake()
        {
            _offset = transform.position - _road.VehicleSplinePosition;
        }

        private void LateUpdate()
        {
            transform.position = _road.VehicleSplinePosition + _offset;
        }
    }
}