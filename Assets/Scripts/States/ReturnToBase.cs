using States.Interfaces;
using Utils;

namespace States
{
    public class ReturnToBase : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _context;
        private const float ARRIVE_THRESHOLD = 2f;
        
        public ReturnToBase(DronStateMachine stateMachine, DroneContext context, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _context = context;
        }
        
        public void OnEnter()
        {
            var targetPos = _context.BaseTransform.position;
            
            _context.Agent.isStopped = false;
            
            _context.Agent.SetDestination(targetPos);
        }

        public void OnExit()
        {
            _context.Agent.isStopped = true;
        }

        public void OnUpdateBehaviour()
        {
            if (_context.Agent.pathPending)
                return;

            if (!(_context.Agent.remainingDistance <= ARRIVE_THRESHOLD)) 
                return;
            
            StateMachine.TrySwapState<UnloadingResource>();
        }

        public void OnFixedUpdateBehaviour()
        {
            
        }

        public bool TrySwapState()
        {
            return _context.HasCargo || _context.TargetResource == null;
        }
    }
}