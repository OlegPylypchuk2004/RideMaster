using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;

namespace CameraManagment
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField, MinValue(1)] private Vector2Int _referenceScreenSize;
        [SerializeField] private CinemachineCamera _cinemachineCamera;

        private float _referenceOrthoSize;

        private void Awake()
        {
            _referenceOrthoSize = _cinemachineCamera.Lens.OrthographicSize;
            ApplyScale();
        }

        private void ApplyScale()
        {
            float currentScreenWidth = Screen.width;
            float scaleFactor = currentScreenWidth / _referenceScreenSize.x;

            _cinemachineCamera.Lens.OrthographicSize = _referenceOrthoSize / scaleFactor;
        }
    }
}
