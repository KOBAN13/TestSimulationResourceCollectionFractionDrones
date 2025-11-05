using States.Interfaces;
using Utils;
using UnityEngine;

namespace States
{
    public class MoveToTarget : IState
    {
        public DronStateMachine StateMachine { get; private set; }
        public string StateName { get; private set; }
        private readonly DroneContext _context;
        private bool _toBase;
        private const float ArriveThreshold = 0.5f;
        
        public MoveToTarget(DronStateMachine stateMachine, DroneContext context, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _context = context;
        }
        
        public void OnEnter()
        {
            // decide target: if carrying cargo -> go to base, else -> go to resource
            _toBase = _context.HasCargo || _context.TargetResource == null;
            
            var targetPos = _toBase ? _context.BaseTransform.position : _context.TargetResource.transform.position;
            
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

            if (!(_context.Agent.remainingDistance <= ArriveThreshold)) 
                return;
            
            if (_toBase)
                StateMachine.TrySwapState<UnloadingResource>();
            else
                StateMachine.TrySwapState<CollectResource>();
        }

        public void OnFixedUpdateBehaviour()
        {
            // no physics-based movement needed for NavMeshAgent
        }

        public bool TrySwapState()
        {
            return _toBase ? _context.HasCargo || _context.TargetResource == null : _context.TargetResource != null;
        }
    }
}