using UnityEngine;

namespace RoadSystem
{
    public class Road : MonoBehaviour
    {
        [field: SerializeField] public Vector3 VehicleStartPoint { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(VehicleStartPoint, 0.25f);
        }
    }
}