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
        private IDroneUnloadingResourceEffect _droneUnloadingResourceEffect;
        private DroneBaseDictionary _droneBaseDictionary;
        
        private DroneSimulationModel _model;
        
        private bool _showDronePath;
        
        [Inject]
        public void Construct(
            DroneSimulationModel model, 
            DroneBaseDictionary droneBaseDictionary, 
            IResourceDirectory resourceDirectory
        )
        {
            _resourceDirectory = resourceDirectory;
            _droneBaseDictionary = droneBaseDictionary;
            _model = model;
        }

        private void Awake()
        {
            _model.DroneSpeed
                .Subscribe(speed => _view.Agent.speed = speed)
                .AddTo(this);
            
            _model.ShowDronePath
                .Subscribe(isActive => _showDronePath = isActive)
                .AddTo(this);

            _droneUnloadingResourceEffect = new DroneUnloadingResourceEffect(_view.ParticleSystem);
            
            var agent = _view.Agent;
            
            var baseTransform = _droneBaseDictionary[_view.Fraction];
            
            _context = new DroneContext(_view, agent, baseTransform, _view.Fraction);

            _dronStateMachine = new DronStateMachine(null);

            var find = new FindResource(_dronStateMachine, _context, _resourceDirectory, "FindResource");
            var move = new MoveToTarget(_dronStateMachine, _context, "MoveToTarget");
            var collect = new CollectResource(_dronStateMachine, _context, _resourceDirectory, "CollectResource");
            var returnBase = new ReturnToBase(_dronStateMachine, _context, "ReturnToBase");
            var unload = new UnloadingResource(_model, _dronStateMachine, _context, _droneUnloadingResourceEffect, "UnloadingResource");

            _stateMachine = new StateMachine(new List<IState>
            {
                find, move, collect, returnBase, unload
            });
            
            _dronStateMachine.SetStateMachine(_stateMachine);
            _stateMachine.SwitchStates<FindResource>();
        }

        private void UpdatePathVisualization()
        {
            var corners = _view.Agent.path.corners;

            if (corners.Length < 2)
            {
                _view.LineRenderer.positionCount = 0;
                return;
            }

            _view.LineRenderer.positionCount = corners.Length;
            _view.LineRenderer.SetPositions(corners);
        }

        private void Update()
        {
            _stateMachine.CurrentStates.OnUpdateBehaviour();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentStates.OnFixedUpdateBehaviour();
            
            if (_view.Agent.path == null || !_showDronePath)
            {
                _view.LineRenderer.positionCount = 0;

                return;
            }

            UpdatePathVisualization();
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