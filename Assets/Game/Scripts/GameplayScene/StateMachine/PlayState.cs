using InputSystem;

namespace GameplayScene.StateMachine
{
    public class PlayState : GameplaySceneState
    {
        private readonly IInputHandler _inputHandler;

        public PlayState(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public override void Enter()
        {
            base.Enter();

            _inputHandler.IsActive = true;
        }

        public override void Exit()
        {
            base.Exit();

            _inputHandler.IsActive = false;
        }

        public override void Update()
        {
            base.Update();

            _inputHandler.Update();
        }
    }
}