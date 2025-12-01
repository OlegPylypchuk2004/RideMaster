using Zenject;

namespace SaveSystem
{
    public class SaveSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SaveManager>()
                .AsSingle();
        }
    }
}