using States.Interfaces;
using Utils;
using Services;
using R3;
using System;

namespace States
{
    public class CollectResource : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _ctx;
        private readonly IResourceDirectory _directory;
        private IDisposable _timer;
        
        public CollectResource(DronStateMachine stateMachine, DroneContext ctx, IResourceDirectory directory, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _ctx = ctx;
            _directory = directory;
        }
        
        public void OnEnter()
        {
            _timer = Observable.Timer(TimeSpan.FromSeconds(2))
                .Subscribe(_ =>
                {
                    _ctx.HasCargo = true;
                    if (_ctx.TargetResource != null)
                    {
                        _directory.Consume(_ctx.TargetResource);
                        _ctx.TargetResource = null;
                    }
                    StateMachine.TrySwapState<ReturnToBase>();
                });
            StateMachine.AddDispose(_timer);
        }

        public void OnExit()
        {
            _timer?.Dispose();
        }

        public void OnUpdateBehaviour()
        {
            
        }

        public void OnFixedUpdateBehaviour()
        {
            
        }

        public bool TrySwapState()
        {
            return _ctx.TargetResource != null || _ctx.HasCargo;
        }
    }
}