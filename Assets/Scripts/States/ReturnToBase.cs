using States.Interfaces;
using Utils;

namespace States
{
    public class ReturnToBase : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _ctx;
        
        public ReturnToBase(DronStateMachine stateMachine, DroneContext ctx, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _ctx = ctx;
        }
        
        public void OnEnter()
        {
            // Delegate movement to MoveToTarget (which will detect _ctx.HasCargo and go to base)
            StateMachine.TrySwapState<MoveToTarget>();
        }

        public void OnExit()
        {
            
        }

        public void OnUpdateBehaviour()
        {
            
        }

        public void OnFixedUpdateBehaviour()
        {
            
        }

        public bool TrySwapState()
        {
            return _ctx.HasCargo;
        }
    }
}