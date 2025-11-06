using States.Interfaces;
using Services;
using Utils;

namespace States
{
    public class FindResource : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _context;
        private readonly IResourceDirectory _directory;
        
        public FindResource(DronStateMachine stateMachine, DroneContext context, IResourceDirectory directory, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _context = context;
            _directory = directory;
        }
        
        public void OnEnter()
        {
            if (_context.TargetResource != null)
            {
                _directory.Release(_context.TargetResource);
                _context.TargetResource = null;
            }
            
            TryFind();
        }

        public void OnExit()
        {
            
        }

        public void OnUpdateBehaviour()
        {
            if (_context.TargetResource == null)
                TryFind();
        }

        public void OnFixedUpdateBehaviour()
        {
            
        }

        public bool TrySwapState()
        {
            return true;
        }

        private void TryFind()
        {
            if (_directory.TryReserveNearest(_context.Position, out var res))
            {
                _context.TargetResource = res;
                StateMachine.TrySwapState<MoveToTarget>();
            }
        }
    }
}