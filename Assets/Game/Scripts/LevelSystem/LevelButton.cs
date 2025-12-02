using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _numberTextMesh;

        private LevelConfig _levelConfig;

        public event Action<LevelConfig> Selected;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            if (levelConfig == null)
            {
                return;
            }

            _levelConfig = levelConfig;
            _numberTextMesh.text = $"{_levelConfig.Number}";
        }

        private void OnButtonClicked()
        {
            Selected?.Invoke(_levelConfig);
        }
    }
}