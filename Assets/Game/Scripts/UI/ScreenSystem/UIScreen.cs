using DG.Tweening;
using System;
using UnityEngine;

namespace UI.ScreenSystem
{
    public class UIScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup[] _canvasGroups;
        [SerializeField, Min(0f)] private float _appearDuration;
        [SerializeField, Min(0f)] private float _appearDelay;
        [SerializeField] private Ease _appearEase;
        [SerializeField, Min(0f)] private float _disappearDuration;
        [SerializeField] private Ease _disappearEase;
        [SerializeField, Min(0f)] private float _maxScale;
        [SerializeField] private bool _isIgnoreTimeScale;

        private Sequence _sequence;

        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void OnDestroy() { }

        public virtual Sequence Appear(Action action = null)
        {
            gameObject.SetActive(true);

            foreach (CanvasGroup canvasGroup in _canvasGroups)
            {
                canvasGroup.interactable = false;
            }

            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.SetUpdate(_isIgnoreTimeScale);
            _sequence.SetLink(gameObject);

            _sequence.AppendInterval(_appearDelay);

            foreach (CanvasGroup canvasGroup in _canvasGroups)
            {
                _sequence.Join(canvasGroup.transform.DOScale(1f, _appearDuration)
                    .From(_maxScale)
                    .SetEase(_appearEase));

                _sequence.Join(canvasGroup.DOFade(1f, _appearDuration)
                    .From(0f)
                    .SetEase(_appearEase));
            }

            _sequence.OnKill(() =>
            {
                foreach (CanvasGroup canvasGroup in _canvasGroups)
                {
                    canvasGroup.interactable = true;
                }

                action?.Invoke();
            });

            return _sequence;
        }

        public virtual Sequence Disappear(Action action = null)
        {
            gameObject.SetActive(true);

            foreach (CanvasGroup canvasGroup in _canvasGroups)
            {
                canvasGroup.interactable = false;
            }

            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.SetUpdate(_isIgnoreTimeScale);
            _sequence.SetLink(gameObject);

            foreach (CanvasGroup canvasGroup in _canvasGroups)
            {
                _sequence.Join(canvasGroup.transform.DOScale(_maxScale, _disappearDuration)
                    .From(1f)
                    .SetEase(_disappearEase));

                _sequence.Join(canvasGroup.DOFade(0f, _disappearDuration)
                    .From(1f)
                    .SetEase(_disappearEase));
            }

            _sequence.OnKill(() =>
            {
                gameObject.SetActive(false);

                action?.Invoke();
            });

            return _sequence;
        }
    }
}