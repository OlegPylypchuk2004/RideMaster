using LevelSystem;
using SessionSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI.ScreenSystem.Screens
{
    public class LevelsUIScreen : UIScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private LocationsUIScreen _locationsUIScreen;
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private RectTransform _levelButtonsParent;

        private SessionData _sessionData;
        private List<LevelButton> _levelButtons;

        [Inject]
        private void Construct(SessionData sessionData)
        {
            _sessionData = sessionData;
        }

        protected override void Awake()
        {
            base.Awake();

            _levelButtons = new List<LevelButton>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            UpdateButtons();

            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _backButton.onClick.RemoveListener(OnBackButtonClicked);

            foreach (LevelButton levelButton in _levelButtons)
            {
                levelButton.Selected -= OnLevelSelected;
            }
        }

        private void OnBackButtonClicked()
        {
            Disappear(() =>
            {
                _locationsUIScreen.Appear();
            });
        }

        private void UpdateButtons()
        {
            foreach (LevelButton levelButton in _levelButtons)
            {
                levelButton.gameObject.SetActive(false);
            }

            LevelConfig[] levelConfigs = _sessionData.locationConfig.LevelConfigs;

            for (int i = 0; i < levelConfigs.Length; i++)
            {
                LevelButton levelButton;

                if (i >= _levelButtons.Count)
                {
                    levelButton = Instantiate(_levelButtonPrefab, _levelButtonsParent);
                    levelButton.Selected += OnLevelSelected;

                    _levelButtons.Add(levelButton);
                }
                else
                {
                    levelButton = _levelButtons[i];
                }

                levelButton.gameObject.SetActive(true);
                levelButton.SetLevelConfig(levelConfigs[i]);
            }
        }

        private void OnLevelSelected(LevelConfig levelConfig)
        {
            if (levelConfig == null)
            {
                return;
            }

            _sessionData.levelConfig = levelConfig;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}