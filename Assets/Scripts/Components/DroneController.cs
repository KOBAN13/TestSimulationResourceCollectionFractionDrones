using System;
using System.Collections.Generic;
using R3;
using Services;
using States;
using States.Interfaces;
using UI;
using UnityEngine;
using Utils;
using Utils.SerializedDictionary;
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
        private DroneBaseDictionary _droneBaseDictionary;
        
        private DroneSimulationModel _model;
        
        [Inject]
        public void Construct(
            DroneSimulationModel model, 
            DroneBaseDictionary droneBaseDictionary, 
            IResourceDirectory resourceDirectory, 
            IEffectPlayer effectPlayer
        )
        {
            _resourceDirectory = resourceDirectory;
            _effectPlayer = effectPlayer;
            _droneBaseDictionary = droneBaseDictionary;
            _model = model;
        }

        private void Awake()
        {
            _model.DroneSpeed
                .Subscribe(speed => _view.Agent.speed = speed)
                .AddTo(this);
            
            var agent = _view.Agent;
            
            var baseTransform = _droneBaseDictionary[_view.Fraction];
            
            _context = new DroneContext(_view, agent, baseTransform, _view.Fraction);

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
            _stateMachine.CurrentStates.OnUpdateBehaviour();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentStates.OnFixedUpdateBehaviour();
        }

        private void OnDestroy()
        {
            if (_context.TargetResource == null)
                return;
            
            _resourceDirectory.Release(_context.TargetResource);
            _context.TargetResource = null;
        }
    }
}