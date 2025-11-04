using System;
using System.Collections.Generic;
using System.Linq;
using State;
using Zenject;

namespace States
{
    public class DronStateMachine : ITickable, IInitializable, IDisposable, IFixedTickable
    {
        private readonly StateMachine _stateMachine;
        private readonly List<IDisposable> _disposables = new();

        public DronStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void AddDispose(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
        
        public StateMachine GetStateMachine() => _stateMachine;

        public void TrySwapState<T>() where T : IState
        {
            if (_stateMachine.CurrentStates is not T)
            {
                var state = _stateMachine.States.Values.FirstOrDefault(typeState => typeState is T);

                if (state == null) 
                    throw new ArgumentNullException("Dont find state");

                if (state.TrySwapState())
                {
                    _stateMachine.SwitchStates<T>();
                }
            }
        }

        public void Tick()
        {
            _stateMachine.CurrentStates.OnUpdateBehaviour();
        }
        
        public void FixedTick()
        {
            _stateMachine.CurrentStates.OnFixedUpdateBehaviour();
        }
        
        public void Initialize()
        {
            /*_states = new()
            {
                new PlayerIdle(this),
                new PlayerMovement(this),
                new PlayerRising(this),
                new PlayerSliding(this),
                new PlayerFalling(this),
                new PlayerJumping(this),
                new PlayerPicksUpItem(this)
            };
            
            _stateMachine = new StateMachine(_states);
            _stateMachine.SwitchStates<PlayerIdle>();*/
        }

        public void Dispose()
        {
            foreach (var dispose in _disposables)
            {
                dispose.Dispose();
            }
        }
    }
}