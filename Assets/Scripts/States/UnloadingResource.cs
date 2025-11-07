using States.Interfaces;
using Utils;
using Services;
using UI;

namespace States
{
    public class UnloadingResource : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _ctx;
        private readonly IDroneUnloadingResourceEffect _effects;
        private readonly DroneSimulationModel _model;
        
        public UnloadingResource(DroneSimulationModel model, DronStateMachine stateMachine, DroneContext ctx, IDroneUnloadingResourceEffect effects, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _ctx = ctx;
            _effects = effects;
            _model = model;
        }
        
        public async void OnEnter()
        {
            await _effects.PlayUnloadEffect(_ctx.BaseTransform.position);
            
            _ctx.HasCargo = false;
            
            UpdateCountResource(_ctx.Fraction);
            
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
        
        //TODO: Вынести, странное решение
        private void UpdateCountResource(EDroneFraction fraction)
        {
            switch (fraction)
            {
                case EDroneFraction.Red:
                    _model.ResourcesCollectedInRedTeam.Value++;
                    break;
                case EDroneFraction.Blue:
                    _model.ResourcesCollectedInBlueTeam.Value++;
                    break;
            }
        }
    }
}