using System;
using System.Collections.Generic;
using States.Interfaces;

namespace States
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states = null;
        public IState CurrentStates { get; private set; }
        public IState PreviouslyState { get; private set; }
        public bool IsUpdate { get; private set; }
        public IReadOnlyDictionary<Type, IState> States => _states;

        public StateMachine(List<IState> states)
        {
            _states = new Dictionary<Type, IState>(states.Count);
            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
            }
        }

        public void SwitchStates<TState>() where TState : IState
        {
            IsUpdate = false;
            TryExitStates();
            GetNewState<TState>();
            TryEnterStates<TState>();
            IsUpdate = true;
        }

        private void TryEnterStates<TState>() where TState : IState
        {
            if (CurrentStates is TState playerBehaviour)
            {
                playerBehaviour.OnEnter();
            }
        }

        private void TryExitStates()
        {
            if (CurrentStates is { } playerBehaviour)
            {
                playerBehaviour.OnExit();
            }
        }

        private void GetNewState<TState>() where TState : IState
        {
            PreviouslyState = CurrentStates;
            var newState = GetState<TState>();
            CurrentStates = newState;
        }

        private TState GetState<TState>() where TState: IState
        {
            return (TState)_states[typeof(TState)];
        }
    }
}