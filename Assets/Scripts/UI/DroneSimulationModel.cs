using R3;

namespace UI
{
    public class DroneSimulationModel
    {
        private readonly ReactiveProperty<int> _droneCountInRedTeam = new();
        private readonly ReactiveProperty<int> _droneCountInBlueTeam = new();
        private readonly ReactiveProperty<float> _droneSpeed = new();
        private readonly ReactiveProperty<float> _resourceSpawnGeneration = new();
        private readonly ReactiveProperty<bool> _showDronePath = new();
        public readonly ReactiveProperty<int> ResourcesCollectedInRedTeam = new();
        public readonly ReactiveProperty<int> ResourcesCollectedInBlueTeam = new();
        
        public ReadOnlyReactiveProperty<int> DroneCountInRedTeam => _droneCountInRedTeam;
        public ReadOnlyReactiveProperty<int> DroneCountInBlueTeam => _droneCountInBlueTeam;
        public ReadOnlyReactiveProperty<float> DroneSpeed => _droneSpeed;
        public ReadOnlyReactiveProperty<float> ResourceSpawnGeneration => _resourceSpawnGeneration;
        public ReadOnlyReactiveProperty<bool> ShowDronePath => _showDronePath;

        public void SetDroneCountInRedTeam(int count)
        {
            _droneCountInRedTeam.Value = count;
        }
        
        public void SetDroneCountInBlueTeam(int count)
        {
            _droneCountInBlueTeam.Value = count;
        }
        
        public void SetDroneSpeed(float speed)
        {
            _droneSpeed.Value = speed;
        }
        
        public void SetResourceSpawnGeneration(float generation)
        {
            _resourceSpawnGeneration.Value = generation;
        }
        
        public void SetShowDronePath(bool isActive)
        {
            _showDronePath.Value = isActive;
        }
        
        public void Dispose()
        {
            _droneCountInRedTeam?.Dispose();
            _droneCountInBlueTeam?.Dispose();
            _droneSpeed?.Dispose();
            _resourceSpawnGeneration?.Dispose();
            _showDronePath?.Dispose();
        }
    }
}