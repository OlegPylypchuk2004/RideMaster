using Patterns.StateMachine;

namespace StateMachine
{
    public abstract class GameState : State
    {
        protected readonly GameStateMachine _stateMachine;

        protected GameState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}