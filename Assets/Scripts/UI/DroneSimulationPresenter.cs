using System;
using DroneFactory.Interfaces;
using R3;

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
            
            _view.OnDroneCountChanged += HandleDroneCountChanged;
            _view.OnDroneSpeedChanged += HandleDroneSpeedChanged;
            
            _model.DroneCount
                .Subscribe(count => _view.UpdateDroneCountDisplay(count))
                .AddTo(_disposables);
            
            _model.DroneSpeed
                .Subscribe(speed => _view.UpdateDroneSpeedDisplay(speed))
                .AddTo(_disposables);
            
            _model.SetDroneCount(1);
            _model.SetDroneSpeed(4);
        }

        private void HandleDroneSpeedChanged(float speedDrone)
        {
            _model.SetDroneSpeed(speedDrone);
        }

        private void HandleDroneCountChanged(int count)
        {
            _model.SetDroneCount(count);
        }
        
        public void Dispose()
        {
            _view.OnDroneCountChanged -= HandleDroneCountChanged;
            _view.OnDroneSpeedChanged -= HandleDroneSpeedChanged;
            
            _disposables?.Dispose();
            _model?.Dispose();
        }
    }
}