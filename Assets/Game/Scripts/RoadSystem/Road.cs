using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using VehicleSystem;
using Zenject;

namespace RoadSystem
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private Vector3 _vehicleStartPoint;
        [SerializeField] private FinishFlag _finishFlag;

        private Vehicle _vehicle;
        private Vector3 _vehicleSplinePosition;
        private float _vehicleSplineTime;

        public Vector3 VehicleSplinePosition => _vehicleSplinePosition;
        public float VehicleSplineTime => _vehicleSplineTime;
        public Vector3 VehicleStartPoint => _vehicleStartPoint;
        public FinishFlag FinishFlag => _finishFlag;

        [Inject]
        private void Construct(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        private void Update()
        {
            if (_vehicle == null || _splineContainer == null)
            {
                return;
            }

            SplineUtility.GetNearestPoint(_splineContainer.Spline, _vehicle.transform.position, out float3 nearestPoint, out float normalizedTime);

            _vehicleSplinePosition = nearestPoint;
            _vehicleSplineTime = normalizedTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(VehicleStartPoint, 0.25f);
        }
    }
}