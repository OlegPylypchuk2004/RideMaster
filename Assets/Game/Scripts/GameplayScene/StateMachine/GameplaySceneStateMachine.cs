using Patterns.StateMachine;
using UnityEngine;
using Zenject;

namespace GameplayScene.StateMachine
{
    public class GameplaySceneStateMachine : StateMachine<GameplaySceneState>, IInitializable, ITickable, IFixedTickable, ILateTickable
    {
        private readonly DiContainer _container;

        public GameplaySceneStateMachine(DiContainer container)
        {
            _container = container;
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