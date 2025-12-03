using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoadSystem
{
    public class DistanceDisplay : MonoBehaviour
    {
        [SerializeField] private Image _progressImage;

        private Road _road;

        [Inject]
        private void Construct(Road road)
        {
            _road = road;
        }

        private void LateUpdate()
        {
            _progressImage.fillAmount = _road.VehicleSplineTime;
        }
    }
}