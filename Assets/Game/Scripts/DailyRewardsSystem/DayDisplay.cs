using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DailyRewardsSystem
{
    public class DayDisplay : MonoBehaviour
    {
        [SerializeField] private DayConfig _dayConfig;
        [SerializeField] Image _backgroundImage;
        [SerializeField] private Sprite _enabledBackgroundSprite;
        [SerializeField] private Sprite _disabledBackgroundSprite;
        [SerializeField] private TMP_Text _dayNumberTextMesh;
        [SerializeField] private Image _rewardIconImage;
        [SerializeField] private TMP_Text _rewardTextMesh;

        private DailyRewards _dailyRewards;

        [Inject]
        private void Construct(DailyRewards dailyRewards)
        {
            _dailyRewards = dailyRewards;
        }

        private void Start()
        {
            UpdateBackgroundSprite();

            _dayNumberTextMesh.text = $"DAY {_dayConfig.Number}";

            _rewardIconImage.sprite = _dayConfig.Reward.GetIcon();
            _rewardIconImage.SetNativeSize();

            _rewardTextMesh.text = _dayConfig.Reward.GetText();
        }

        private void UpdateBackgroundSprite()
        {
            if (_dailyRewards.IsRewardClaimed(_dayConfig) || _dailyRewards.DayNumber == _dayConfig.Number && _dailyRewards.IsCanClaimReward(_dayConfig))
            {
                _backgroundImage.sprite = _enabledBackgroundSprite;
            }
            else
            {
                _backgroundImage.sprite = _disabledBackgroundSprite;
            }
        }
    }
}