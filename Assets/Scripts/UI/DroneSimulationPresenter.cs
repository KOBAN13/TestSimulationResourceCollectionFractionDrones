using System;
using R3;
using UnityEngine;

namespace UI
{
    public class DroneSimulationPresenter : IDisposable
    {
        private readonly DroneSimulationModel _model;
        
        private DroneSimulationView _view;
        private readonly CompositeDisposable _disposables = new();
        
        public DroneSimulationPresenter(DroneSimulationModel model)
        {
            _model = model;
        }
        
        public void Initialize(DroneSimulationView view)
        {
            _view = view;
            
            _view.OnDroneCountRedTeamChanged += HandleDroneCountRedTeamChanged;
            _view.OnDroneCountBlueTeamChanged += HandleDroneCountBlueTeamChanged;
            _view.OnDroneSpeedChanged += HandleDroneSpeedChanged;
            _view.OnResourceSpawnGenerationChanged += HandleResourceSpawnGenerationChanged;
            _view.OnShowDronePath += HandleShowDronePath;
            
            _model.DroneCountInRedTeam
                .Subscribe(count => _view.UpdateDroneCountInRedTeamDisplay(count))
                .AddTo(_disposables);
            
            _model.DroneCountInBlueTeam
                .Subscribe(count => _view.UpdateDroneCountInBlueTeamDisplay(count))
                .AddTo(_disposables);
            
            _model.DroneSpeed
                .Subscribe(speed => _view.UpdateDroneSpeedDisplay(speed))
                .AddTo(_disposables);
            
            _model.ResourcesCollectedInRedTeam
                .Subscribe(count => _view.UpdateResourcesCollectedRedTeamDisplay(count))
                .AddTo(_disposables);
            
            _model.ResourcesCollectedInBlueTeam
                .Subscribe(count => _view.UpdateResourcesCollectedBlueTeamDisplay(count))
                .AddTo(_disposables);
            
            _model.SetDroneCountInRedTeam(1);
            _model.SetDroneCountInBlueTeam(1);
            _model.SetDroneSpeed(4);
            _model.SetResourceSpawnGeneration(3);
            _model.SetShowDronePath(true);
        }

        private void HandleDroneCountBlueTeamChanged(int count)
        {
            _model.SetDroneCountInBlueTeam(count);
        }

        private void HandleShowDronePath(bool isActive)
        {
            _model.SetShowDronePath(isActive);
        }

        private void HandleDroneSpeedChanged(float speedDrone)
        {
            _model.SetDroneSpeed(speedDrone);
        }

        private void HandleDroneCountRedTeamChanged(int count)
        {
            _model.SetDroneCountInRedTeam(count);
        }
        
        private void HandleResourceSpawnGenerationChanged(string generation)
        {
            if (float.TryParse(generation, out var floatGeneration))
            {
                var absGeneration = Mathf.Abs(floatGeneration);
                _model.SetResourceSpawnGeneration(absGeneration);
            }
        }
        
        public void Dispose()
        {
            _view.OnDroneCountRedTeamChanged -= HandleDroneCountRedTeamChanged;
            _view.OnDroneCountBlueTeamChanged -= HandleDroneCountBlueTeamChanged;
            _view.OnDroneSpeedChanged -= HandleDroneSpeedChanged;
            _view.OnResourceSpawnGenerationChanged -= HandleResourceSpawnGenerationChanged;
            _view.OnShowDronePath -= HandleShowDronePath;
            
            _disposables?.Dispose();
            _model?.Dispose();
        }
    }
}