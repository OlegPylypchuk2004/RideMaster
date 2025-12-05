using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PopupSystem
{
    public class Popup : MonoBehaviour
    {
        [Header("Background")]
        [SerializeField, Min(0f)] private float _backgroundImageAppearDuration;
        [SerializeField] private Ease _backgroundImageAppearEase;
        [SerializeField, Range(0f, 1f)] private float _backgroundImageMaxAlpha;
        [SerializeField, Min(0f)] private float _backgroundImageDisappearDuration;
        [SerializeField] private Ease _backgroundImageDisappearEase;
        [SerializeField] private Image _backgroundImage;

        [Header("Canvas group")]
        [SerializeField, Min(0f)] private float _canvasGroupAppearDuration;
        [SerializeField] private Ease _canvasGroupAppearEase;
        [SerializeField, Range(0f, 1f)] private float _canvasGroupMinScale;
        [SerializeField, Min(0f)] private float _canvasGroupDisappearDuration;
        [SerializeField] private Ease _canvasGroupDisappearEase;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Space(25f)]
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

            _canvasGroup.interactable = false;

            _sequence?.Kill(true);
            _sequence = DOTween.Sequence();
            _sequence.SetUpdate(_isIgnoreTimeScale);
            _sequence.SetLink(gameObject);

            _sequence.Append(_backgroundImage.DOFade(_backgroundImageMaxAlpha, _backgroundImageAppearDuration)
                .From(0f)
                .SetEase(_backgroundImageAppearEase));

            _sequence.Append(_canvasGroup.transform.DOScale(1f, _canvasGroupAppearDuration)
                .From(_canvasGroupMinScale)
                .SetEase(_canvasGroupAppearEase));

            _sequence.Join(_canvasGroup.DOFade(1f, _canvasGroupAppearDuration)
                .From(0f)
                .SetEase(_canvasGroupAppearEase));

            _sequence.OnKill(() =>
            {
                _canvasGroup.interactable = true;

                action?.Invoke();
            });

            return _sequence;
        }

        public virtual Sequence Disappear(Action action = null)
        {
            gameObject.SetActive(true);

            _canvasGroup.interactable = false;

            _sequence?.Kill(true);
            _sequence = DOTween.Sequence();
            _sequence.SetUpdate(_isIgnoreTimeScale);
            _sequence.SetLink(gameObject);

            _sequence.Append(_canvasGroup.transform.DOScale(_canvasGroupMinScale, _canvasGroupDisappearDuration)
                .SetEase(_canvasGroupDisappearEase));

            _sequence.Join(_canvasGroup.DOFade(0f, _canvasGroupDisappearDuration)
                .SetEase(_canvasGroupDisappearEase));

            _sequence.Append(_backgroundImage.DOFade(1f, _backgroundImageDisappearDuration)
                .From(0f)
                .SetEase(_backgroundImageDisappearEase));

            _sequence.OnKill(() =>
            {
                gameObject.SetActive(false);

                action?.Invoke();
            });

            return _sequence;
        }
    }
}