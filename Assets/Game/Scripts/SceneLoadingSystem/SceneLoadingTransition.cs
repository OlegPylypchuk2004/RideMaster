using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace SceneLoadingSystem
{
    public class SceneLoadingTransition : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _targetAlpha;
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Image _backgroundImage;

        private SceneLoader _sceneLoader;
        private Sequence _currentSequence;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            Disappear();
        }

        private void OnEnable()
        {
            _sceneLoader.LoadStarted += OnSceneLoadStarted;
        }

        private void OnDisable()
        {
            _sceneLoader.LoadStarted -= OnSceneLoadStarted;
        }

        private void OnSceneLoadStarted()
        {
            Appear();
        }

        private Sequence Appear()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                if (_eventSystem != null)
                {
                    _eventSystem.gameObject.SetActive(false);
                }

                _backgroundImage.gameObject.SetActive(true);
            });

            _currentSequence.Append(_backgroundImage.DOFade(_targetAlpha, _duration)
                .From(0f)
                .SetEase(Ease.OutQuad));

            return _currentSequence;
        }

        private Sequence Disappear()
        {
            _currentSequence?.Kill();

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                if (_eventSystem != null)
                {
                    _eventSystem.gameObject.SetActive(false);
                }

                _backgroundImage.gameObject.SetActive(true);
            });

            _currentSequence.Append(_backgroundImage.DOFade(0f, _duration)
                .SetEase(Ease.InQuad));

            _currentSequence.AppendCallback(() =>
            {
                if (_eventSystem != null)
                {
                    _eventSystem.gameObject.SetActive(true);
                }

                _backgroundImage.gameObject.SetActive(false);
            });

            return _currentSequence;
        }
    }
}