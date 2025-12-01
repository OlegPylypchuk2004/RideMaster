namespace Patterns.StateMachine
{
    public abstract class StateMachine<TState> where TState : State
    {
        public TState CurrentState { get; protected set; }

        public abstract void ChangeState<T>() where T : TState;
    }
}