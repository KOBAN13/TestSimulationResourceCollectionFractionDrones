using States.Interfaces;
using Services;
using Utils;

namespace States
{
    public class FindResource : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _ctx;
        private readonly IResourceDirectory _directory;
        
        public FindResource(DronStateMachine stateMachine, DroneContext ctx, IResourceDirectory directory, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _ctx = ctx;
            _directory = directory;
        }
        
        public void OnEnter()
        {
            TryFind();
        }

        public void OnExit()
        {
            
        }

        public void OnUpdateBehaviour()
        {
            if (_ctx.TargetResource == null)
                TryFind();
        }

        public void OnFixedUpdateBehaviour()
        {
            
        }

        public bool TrySwapState()
        {
            return true;
        }
        
        public void Dispose()
        {
            
        }

        private void TryFind()
        {
            if (_directory.TryReserveNearest(_ctx.Position, out var res))
            {
                _ctx.TargetResource = res;
                StateMachine.TrySwapState<MoveToTarget>();
            }
        }
    }
}