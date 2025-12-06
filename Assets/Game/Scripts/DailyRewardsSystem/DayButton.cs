using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DailyRewardsSystem
{
    public class DayButton : MonoBehaviour
    {
        [SerializeField] private DayConfig _dayConfig;
        [SerializeField] private Button _button;

        private DailyRewards _dailyRewards;

        [Inject]
        private void Construct(DailyRewards dailyRewards)
        {
            _dailyRewards = dailyRewards;
        }

        private void Start()
        {
            UpdateInteractible();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);

            _dailyRewards.RewardClaimed += OnRewardClaimed;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);

            _dailyRewards.RewardClaimed -= OnRewardClaimed;
        }

        private void OnButtonClicked()
        {
            _dailyRewards.TryClaim(_dayConfig);
        }

        private void OnRewardClaimed(DayConfig dayConfig)
        {
            UpdateInteractible();
        }

        private void UpdateInteractible()
        {
            _button.interactable = _dailyRewards.IsCanClaimReward(_dayConfig);
        }
    }
}