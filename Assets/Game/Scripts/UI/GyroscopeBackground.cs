using UnityEngine;

public class GyroscopeBackground : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _maxPixelsOffset;
    [SerializeField] private float _smoothing;

    private Vector2 _initialAnchoredPosition;
    private Vector2 _desiredAnchoredPosition;
    private bool _isGyroSupported;

    private void Start()
    {
        _isGyroSupported = SystemInfo.supportsGyroscope;

        if (_isGyroSupported)
        {
            Input.gyro.enabled = true;
        }

        _initialAnchoredPosition = _rectTransform.anchoredPosition;
    }

    private void Update()
    {
        if (!_isGyroSupported)
        {
            return;
        }

        UpdateTargetPosition();
        ApplySmoothing();
    }

    private void UpdateTargetPosition()
    {
        Vector3 rotationRate = Input.gyro.rotationRateUnbiased;

        float offsetX = Mathf.Clamp(rotationRate.y, -1f, 1f) * _maxPixelsOffset;
        float offsetY = Mathf.Clamp(rotationRate.x, -1f, 1f) * _maxPixelsOffset;

        _desiredAnchoredPosition = _initialAnchoredPosition + new Vector2(offsetX, offsetY);
    }

    private void ApplySmoothing()
    {
        _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _desiredAnchoredPosition, Time.deltaTime * _smoothing);
    }
}