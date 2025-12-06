using UnityEngine;

namespace DailyRewardsSystem
{
    [CreateAssetMenu(fileName = "DayConfig", menuName = "Configs/Daily rewards/Day")]
    public class DayConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int Number { get; private set; }
        [field: SerializeReference] public DayReward Reward { get; private set; }
    }
}