using SessionSystem;
using Zenject;

namespace RoadSystem
{
    public class RoadInstaller : MonoInstaller
    {
        private Road _roadPrefab;

        [Inject]
        private void Construct(SessionData sessionData)
        {
            _roadPrefab = sessionData.levelConfig.RoadPrefab;
        }

        public override void InstallBindings()
        {
            Container.Bind<Road>()
                .FromComponentInNewPrefab(_roadPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}