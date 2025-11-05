using System.Collections.Generic;
using Services;
using States;
using States.Interfaces;
using UnityEngine;
using Utils;
using Zenject;

namespace Components
{
    public class DroneController : MonoBehaviour
    {
        [SerializeField] private DroneView _view;
        
        private DronStateMachine _dronStateMachine;
        private StateMachine _stateMachine;
        private DroneContext _context;
        
        private IResourceDirectory _resourceDirectory;
        private IEffectPlayer _effectPlayer;
        
        [Inject]
        public void Construct(IResourceDirectory resourceDirectory, IEffectPlayer effectPlayer)
        {
            _resourceDirectory = resourceDirectory;
            _effectPlayer = effectPlayer;
        }

        private void Awake()
        {
            var agent = _view.Agent;
            
            _context = new DroneContext(_view, agent, _view.BaseTransform, _view.Fraction);

            _dronStateMachine = new DronStateMachine(null);

            var find = new FindResource(_dronStateMachine, _context, _resourceDirectory, "FindResource");
            var move = new MoveToTarget(_dronStateMachine, _context, "MoveToTarget");
            var collect = new CollectResource(_dronStateMachine, _context, _resourceDirectory, "CollectResource");
            var returnBase = new ReturnToBase(_dronStateMachine, _context, "ReturnToBase");
            var unload = new UnloadingResource(_dronStateMachine, _context, _effectPlayer, "UnloadingResource");

            _stateMachine = new StateMachine(new List<IState>
            {
                find, move, collect, returnBase, unload
            });
            
            _dronStateMachine.SetStateMachine(_stateMachine);
            _stateMachine.SwitchStates<FindResource>();
        }

        private void Update()
        {
            _stateMachine?.CurrentStates?.OnUpdateBehaviour();
        }

        private void FixedUpdate()
        {
            _stateMachine?.CurrentStates?.OnFixedUpdateBehaviour();
        }
    }
}