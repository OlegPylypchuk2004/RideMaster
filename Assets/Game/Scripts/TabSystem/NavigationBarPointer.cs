using DG.Tweening;
using UnityEngine;

namespace TabSystem
{
    public class NavigationBarPointer : MonoBehaviour
    {
        [SerializeField] private TabButton[] _tabButtons;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField, Min(0f)] private float _moveDuration;
        [SerializeField] private Ease _moveEase;

        private Tween _currentTween;

        private void OnEnable()
        {
            foreach (TabButton tabButton in _tabButtons)
            {
                tabButton.Selected += OnButtonSelected;
            }
        }

        private void OnDisable()
        {
            foreach (TabButton tabButton in _tabButtons)
            {
                tabButton.Selected -= OnButtonSelected;
            }
        }

        private void OnButtonSelected(TabButton tabButton)
        {
            _currentTween?.Kill();

            _currentTween = _rectTransform.DOMoveX(tabButton.GetComponent<RectTransform>().position.x, _moveDuration)
                .SetEase(_moveEase)
                .SetLink(gameObject);
        }
    }
}