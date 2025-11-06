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
        private const float ARRIVE_THRESHOLD = 1f;
        
        public MoveToTarget(DronStateMachine stateMachine, DroneContext context, string stateName)
        {
            StateMachine = stateMachine;
            StateName = stateName;
            _context = context;
        }
        
        public void OnEnter()
        {
            var targetPos = _context.TargetResource.transform.position;
            
            _context.Agent.isStopped = false;
            
            _context.Agent.SetDestination(targetPos);
        }

        public void OnExit()
        {
            _context.Agent.isStopped = true;
        }

        public void OnUpdateBehaviour()
        {
            if (_context.TargetResource == null)
            {
                StateMachine.TrySwapState<FindResource>();
                return;
            }
            
            if (_context.Agent.pathPending)
                return;

            if (!(_context.Agent.remainingDistance <= ARRIVE_THRESHOLD)) 
                return;
            
            StateMachine.TrySwapState<CollectResource>();
        }

        public void OnFixedUpdateBehaviour()
        {
            
        }

        public bool TrySwapState()
        {
            return _context.TargetResource != null;
        }
    }
}