using CameraSystem;
using Patterns.StateMachine;
using SaveSystem;
using SessionSystem;
using StateMachine.States;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class GameStateMachine : StateMachine<GameState>, IInitializable, ITickable, IFixedTickable, ILateTickable
    {
        private readonly DiContainer _container;
        private readonly SaveManager _saveManager;
        private readonly CameraMover _cameraMover;
        private readonly SessionData _sessionData;

        public GameStateMachine(DiContainer container, SaveManager saveManager, CameraMover cameraMover, SessionData sessionData)
        {
            _container = container;
            _saveManager = saveManager;
            _cameraMover = cameraMover;
            _sessionData = sessionData;
        }

        public override void ChangeState<T>()
        {
            CurrentState?.Exit();
            CurrentState = _container.Resolve<T>();
            CurrentState?.Enter();

            Debug.Log($"[Game State Machine] State changed: {CurrentState.GetType().Name}");
        }

        public void Initialize()
        {
            if (_saveManager.Data.isTutorialCompleted)
            {
                if (_sessionData.isLoadGameplayImmediately)
                {
                    _cameraMover.SetPosition(_cameraMover.DefaultPosition);
                    ChangeState<GameplayState>();
                }
                else
                {
                    _sessionData.isLoadGameplayImmediately = true;
                    ChangeState<MenuState>();
                }
            }
            else
            {
                _cameraMover.SetPosition(_cameraMover.DefaultPosition);
                ChangeState<GameplayState>();
            }
        }

        public virtual void Tick()
        {
            CurrentState?.Update();
        }

        public virtual void FixedTick()
        {
            CurrentState?.FixedUpdate();
        }

        public virtual void LateTick()
        {
            CurrentState?.LateUpdate();
        }
    }
}