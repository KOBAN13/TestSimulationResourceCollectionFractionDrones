using States.Interfaces;
using Utils;
using UnityEngine;

namespace States
{
    public class MoveToTarget : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _ctx;
        private bool _toBase;
        private const float ArriveThreshold = 0.5f;
        
        public MoveToTarget(DronStateMachine stateMachine, DroneContext ctx, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _ctx = ctx;
        }
        
        public void Dispose()
        {
            
        }
        
        public void OnEnter()
        {
            // decide target: if carrying cargo -> go to base, else -> go to resource
            _toBase = _ctx.HasCargo || _ctx.TargetResource == null;
            Vector3 targetPos = _toBase ? _ctx.BaseTransform.position : _ctx.TargetResource.transform.position;
            _ctx.Agent.isStopped = false;
            _ctx.Agent.SetDestination(targetPos);
        }

        public void OnExit()
        {
            _ctx.Agent.isStopped = true;
        }

        public void OnUpdateBehaviour()
        {
            if (_ctx.Agent.pathPending) return;
            if (_ctx.Agent.remainingDistance <= ArriveThreshold)
            {
                if (_toBase)
                    StateMachine.TrySwapState<UnloadingResource>();
                else
                    StateMachine.TrySwapState<CollectResource>();
            }
        }

        public void OnFixedUpdateBehaviour()
        {
            // no physics-based movement needed for NavMeshAgent
        }

        public bool TrySwapState()
        {
            return _toBase ? _ctx.HasCargo || _ctx.TargetResource == null : _ctx.TargetResource != null;
        }
    }
}