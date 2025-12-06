using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DailyRewardsSystem
{
    public class DayDisplay : MonoBehaviour
    {
        [SerializeField] private DayConfig _dayConfig;
        [SerializeField] private TMP_Text _dayNumberTextMesh;
        [SerializeField] private Image _rewardIconImage;
        [SerializeField] private TMP_Text _rewardTextMesh;

        private void Start()
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (_dayConfig == null)
            {
                return;
            }

            _dayNumberTextMesh.text = $"DAY {_dayConfig.Number}";

            _rewardIconImage.sprite = _dayConfig.Reward.GetIcon();
            _rewardIconImage.SetNativeSize();

            _rewardTextMesh.text = _dayConfig.Reward.GetText();
        }
    }
}