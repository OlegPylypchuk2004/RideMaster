using Patterns.StateMachine;

namespace GameplayScene.StateMachine
{
    public abstract class GameplaySceneState : State
    {
        protected GameplaySceneStateMachine _stateMachine;

        protected GameplaySceneState(GameplaySceneStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}