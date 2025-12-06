using Zenject;

namespace DailyRewardsSystem
{
    public class DailyRewardsSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DailyRewards>()
                .AsSingle();
        }
    }
}