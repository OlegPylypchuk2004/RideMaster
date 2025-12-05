using SceneLoadingSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameplayScene
{
    public class GameplaySceneUIManager : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            _sceneLoader.Load(_sceneLoader.ActiveSceneIndex - 1);
        }
    }
}