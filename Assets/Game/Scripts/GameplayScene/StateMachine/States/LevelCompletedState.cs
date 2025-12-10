using UI.PopupSystem.Popups;

namespace GameplayScene.StateMachine.States
{
    public class LevelCompletedState : GameplaySceneState
    {
        private readonly LevelCompletedPopup _popup;

        public LevelCompletedState(GameplaySceneStateMachine stateMachine, LevelCompletedPopup popup) : base(stateMachine)
        {
            _popup = popup;
        }

        public override void Enter()
        {
            base.Enter();

            _popup.Appear();
        }
    }
}