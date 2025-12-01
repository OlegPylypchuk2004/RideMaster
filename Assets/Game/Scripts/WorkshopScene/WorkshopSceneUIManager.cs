using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vehicle.Building;

namespace WorkshopScene
{
    public class WorkshopSceneUIManager : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private VehicleBuilder _vehicleBuilder;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _resetButton.onClick.AddListener(OnResetButtonClicked);
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _resetButton.onClick.RemoveListener(OnResetButtonClicked);
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
        }

        private void OnBackButtonClicked()
        {

        }

        private void OnResetButtonClicked()
        {

        }

        private void OnStartButtonClicked()
        {
            _vehicleBuilder.Build();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}