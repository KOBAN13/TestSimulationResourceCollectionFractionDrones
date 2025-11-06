using System;
using DroneFactory.Interfaces;
using R3;

namespace UI
{
    public class DroneSimulationPresenter : IDisposable
    {
        private readonly DroneSimulationModel _model;
        private readonly IDroneSpawner _droneSpawner;
        
        private DroneSimulationView _view;
        private readonly CompositeDisposable _disposables = new();
        
        public DroneSimulationPresenter(
            DroneSimulationModel model,
            IDroneSpawner droneSpawner)
        {
            _model = model;
            _droneSpawner = droneSpawner;
        }
        
        public void Initialize(DroneSimulationView view)
        {
            _view = view;
            
            _view.OnDroneCountChanged += HandleDroneCountChanged;
            _view.OnDroneSpeedChanged += HandleDroneSpeedChanged;
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
            if (_view != null)
            {
                _view.OnDroneCountChanged -= HandleDroneCountChanged;
            }
            
            _disposables?.Dispose();
            _model?.Dispose();
        }
    }
}