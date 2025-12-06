using SceneLoadingSystem;
using UnityEngine;
using UnityEngine.UI;
using VehicleSystem.Building;
using Zenject;

namespace WorkshopScene
{
    public class WorkshopSceneUIManager : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private VehicleBuilder _vehicleBuilder;
        [SerializeField] private PartSectionsPanel _partSectionsPanel;
        [SerializeField] private PartButtonsPanel _partButtonsPanel;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

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

        private void Update()
        {
            _startButton.interactable = _vehicleBuilder.IsCanBuildVehicle();
            _resetButton.interactable = _vehicleBuilder.IsCanResetVehicle();
        }

        private void OnBackButtonClicked()
        {
            _sceneLoader.Load(_sceneLoader.ActiveSceneIndex - 1);
        }

        private void OnResetButtonClicked()
        {
            _vehicleBuilder.ResetVehicle();
            _partSectionsPanel.ResetSections();
            _partButtonsPanel.ResetButtons();
        }

        private void OnStartButtonClicked()
        {
            _vehicleBuilder.BuildVehicle();
            _sceneLoader.Load(_sceneLoader.ActiveSceneIndex + 1);
        }
    }
}