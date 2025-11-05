using States.Interfaces;
using Utils;
using Services;

namespace States
{
    public class UnloadingResource : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _ctx;
        private readonly IEffectPlayer _effects;
        
        public UnloadingResource(DronStateMachine stateMachine, DroneContext ctx, IEffectPlayer effects, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _ctx = ctx;
            _effects = effects;
        }
        
        public void OnEnter()
        {
            _effects.PlayUnloadEffect(_ctx.BaseTransform.position);
            
            _ctx.HasCargo = false;
            
            StateMachine.TrySwapState<FindResource>();
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
            return true;
        }
    }
}