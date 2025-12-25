using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SceneLoadingSystem
{
    public class SceneLoadingTransition : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _targetAlpha;
        [SerializeField, MinValue(0f)] private float _appearDuration;
        [SerializeField] private Ease _appearEase;
        [SerializeField, MinValue(0f)] private float _disappearDuration;
        [SerializeField] private Ease _disappearEase;
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
            _currentSequence?.Kill(true);

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                _backgroundImage.gameObject.SetActive(true);
            });

            _currentSequence.Append(_backgroundImage.DOFade(_targetAlpha, _appearDuration)
                .From(0f)
                .SetEase(_appearEase));

            return _currentSequence;
        }

        private Sequence Disappear()
        {
            _currentSequence?.Kill(true);

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject);

            _currentSequence.AppendCallback(() =>
            {
                _backgroundImage.gameObject.SetActive(true);
            });

            _currentSequence.Append(_backgroundImage.DOFade(0f, _disappearDuration)
                .SetEase(_disappearEase));

            _currentSequence.AppendCallback(() =>
            {
                _backgroundImage.gameObject.SetActive(false);
            });

            return _currentSequence;
        }
    }
}