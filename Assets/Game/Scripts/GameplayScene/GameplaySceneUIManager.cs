using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameplayScene
{
    public class GameplaySceneUIManager : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}