using InputSystem;
using RoadSystem;
using VehicleSystem.Parts.Gameplay;

namespace GameplayScene.StateMachine.States
{
    public class PlayState : GameplaySceneState
    {
        private readonly IInputHandler _inputHandler;
        private readonly FinishFlag _finishFlag;

        public PlayState(GameplaySceneStateMachine stateMachine, IInputHandler inputHandler, Road road) : base(stateMachine)
        {
            _inputHandler = inputHandler;
            _finishFlag = road.FinishFlag;
        }

        public override void Enter()
        {
            base.Enter();

            _inputHandler.IsActive = true;
            _finishFlag.VehicleBasePartTriggered += OnFinishFlagReached;
        }

        public override void Exit()
        {
            base.Exit();

            _inputHandler.IsActive = false;
            _finishFlag.VehicleBasePartTriggered -= OnFinishFlagReached;
        }

        private void OnFinishFlagReached(GameplayBasePart gameplayBasePart)
        {
            _stateMachine.ChangeState<VictoryState>();
        }
    }
}