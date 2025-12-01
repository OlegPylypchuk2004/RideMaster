using StateMachine.States;
using Zenject;

namespace StateMachine
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle()
                .NonLazy();

            Container.Bind<MenuState>()
                .AsSingle();

            Container.Bind<GameplayState>()
                .AsSingle();

            Container.Bind<SettingsState>()
                .AsSingle();

            Container.Bind<MovesAreLeftState>()
                .AsSingle();

            Container.Bind<ItemsCollectedState>()
                .AsSingle();
        }
    }
}