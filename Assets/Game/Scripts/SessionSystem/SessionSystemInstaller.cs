using Zenject;

namespace SessionSystem
{
    public class SessionSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SessionData>()
                .AsSingle();
        }
    }
}