using UnityEngine;
using UnityEngine.Splines;

namespace RoadSystem
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private SplineContainer[] _splineContainers;
        [SerializeField] private Vector3 _vehicleStartPoint;
        [SerializeField] private FinishFlag _finishFlag;

        public Vector3 VehicleStartPoint => _vehicleStartPoint;
        public FinishFlag FinishFlag => _finishFlag;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_vehicleStartPoint, 0.25f);
        }
    }
}