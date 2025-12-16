using Unity.Cinemachine;
using UnityEngine;
using VehicleSystem;
using Zenject;

namespace CameraManagment
{
    public class VehicleFollowCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;

        private Vehicle _vehicle;

        [Inject]
        private void Construct(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        private void Start()
        {
            _cinemachineCamera.Target.TrackingTarget = _vehicle.transform;
        }
    }
}