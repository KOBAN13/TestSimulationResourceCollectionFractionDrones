using System;
using System.Collections.Generic;
using System.Linq;
using States.Interfaces;

namespace States
{
    public class DronStateMachine
    {
        private StateMachine _stateMachine;
        private readonly List<IDisposable> _disposables = new();

        public DronStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void SetStateMachine(StateMachine stateMachine)
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
    }
}