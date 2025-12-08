using SessionSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace LevelSystem
{
    public class LevelNumberDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textMesh;

        private SessionData _sessionData;

        [Inject]
        private void Construct(SessionData sessionData)
        {
            _sessionData = sessionData;
        }

        private void Start()
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            LevelConfig levelConfig = _sessionData.levelConfig;

            if (levelConfig == null)
            {
                _textMesh.text = string.Empty;
            }
            else
            {
                _textMesh.text = $"LEVEL {_sessionData.levelConfig.Number}";
            }
        }
    }
}