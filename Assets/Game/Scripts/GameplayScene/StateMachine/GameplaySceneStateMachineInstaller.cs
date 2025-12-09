using Zenject;

namespace GameplayScene.StateMachine
{
    public class GameplaySceneStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplaySceneStateMachine>()
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayState>()
                .AsSingle();
        }
    }
}