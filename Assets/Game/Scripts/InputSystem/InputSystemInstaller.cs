using Zenject;

namespace InputSystem
{
    public class InputSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>()
                .To<TouchInputHandler>()
                .AsSingle();
        }
    }
}