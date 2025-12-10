using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using VehicleSystem;
using Zenject;

namespace RoadSystem
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private SplineContainer[] _splineContainers;
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
            if (_vehicle == null || _splineContainers == null || _splineContainers.Length == 0)
            {
                return;
            }

            float minDistance = float.MaxValue;
            float3 bestPoint = float3.zero;
            float bestTime = 0f;

            Vector3 vehiclePosition = _vehicle.transform.position;

            foreach (SplineContainer container in _splineContainers)
            {
                if (container == null)
                {
                    continue;
                }

                SplineUtility.GetNearestPoint(container.Spline, vehiclePosition, out float3 point, out float time);
                float distance = math.distancesq(vehiclePosition, point);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestPoint = point;
                    bestTime = time;
                }
            }

            _vehicleSplinePosition = bestPoint;
            _vehicleSplineTime = bestTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_vehicleStartPoint, 0.25f);
        }
    }
}