using UnityEngine;
using UnityEngine.UI;
using VehicleSystem;
using Zenject;

namespace RoadSystem
{
    public class DistanceDisplay : MonoBehaviour
    {
        [SerializeField] private Image _progressImage;

        private Road _road;
        private Vehicle _vehicle;

        [Inject]
        private void Construct(Road road, Vehicle vehicle)
        {
            _road = road;
            _vehicle = vehicle;
        }

        private void LateUpdate()
        {
            _progressImage.fillAmount = Mathf.InverseLerp(_road.VehicleStartPoint.z, _road.FinishFlag.transform.position.z, _vehicle.transform.position.z);
        }
    }
}