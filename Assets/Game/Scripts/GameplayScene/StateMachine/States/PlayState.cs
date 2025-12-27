using InputSystem;
using RoadSystem;
using VehicleSystem;
using VehicleSystem.Parts.Gameplay;

namespace GameplayScene.StateMachine.States
{
    public class PlayState : GameplaySceneState
    {
        private readonly IInputHandler _inputHandler;
        private readonly FinishFlag _finishFlag;
        private readonly VehicleStopChecker _vehicleStopChecker;

        public PlayState(GameplaySceneStateMachine stateMachine, IInputHandler inputHandler, Road road, VehicleStopChecker vehicleStopChecker) : base(stateMachine)
        {
            _inputHandler = inputHandler;
            _finishFlag = road.FinishFlag;
            _vehicleStopChecker = vehicleStopChecker;
        }

        public override void Enter()
        {
            base.Enter();

            _inputHandler.IsActive = true;
            _vehicleStopChecker.IsActive = true;
            _finishFlag.VehicleBasePartTriggered += OnFinishFlagReached;
        }

        public override void Exit()
        {
            base.Exit();

            _inputHandler.IsActive = false;
            _vehicleStopChecker.IsActive = false;
            _finishFlag.VehicleBasePartTriggered -= OnFinishFlagReached;
        }

        private void OnFinishFlagReached(GameplayBasePart gameplayBasePart)
        {
            _stateMachine.ChangeState<LevelCompletedState>();
        }
    }
}