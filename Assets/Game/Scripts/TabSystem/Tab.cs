using UnityEngine;

namespace TabSystem
{
    public class Tab : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;

        public virtual void Activate()
        {
            _canvasGroup.gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            _canvasGroup.gameObject.SetActive(false);
        }

        public void ApplySize(float width, float height)
        {
            _rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
}