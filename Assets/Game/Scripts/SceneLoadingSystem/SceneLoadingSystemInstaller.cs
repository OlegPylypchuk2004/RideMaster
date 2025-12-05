using Zenject;

namespace SceneLoadingSystem
{
    public class SceneLoadingSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>()
                .AsSingle();
        }
    }
}