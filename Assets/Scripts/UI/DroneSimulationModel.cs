using R3;

namespace UI
{
    public class DroneSimulationModel
    {
        private readonly ReactiveProperty<int> _droneCount = new();
        private readonly ReactiveProperty<float> _droneSpeed = new(0);
        
        public ReadOnlyReactiveProperty<int> DroneCount => _droneCount;
        public ReadOnlyReactiveProperty<float> DroneSpeed => _droneSpeed;
        
        public void SetDroneCount(int count)
        {
            _droneCount.Value = count;
        }
        
        public void SetDroneSpeed(float speed)
        {
            _droneSpeed.Value = speed;
        }
        
        public void Dispose()
        {
            _droneCount?.Dispose();
            _droneSpeed?.Dispose();
        }
    }
}