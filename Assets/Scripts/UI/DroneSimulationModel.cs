using System;
using R3;

namespace UI
{
    public class DroneSimulationModel
    {
        private readonly ReactiveProperty<int> _droneCount = new(0);
        private readonly ReactiveProperty<float> _droneSpeed = new(0);
        
        public ReadOnlyReactiveProperty<int> DroneCount => _droneCount;
        public ReadOnlyReactiveProperty<float> DroneSpeed => _droneSpeed;
        
        public int MaxDroneCount { get; private set; } = 5;
        public float MaxDroneSpeed { get; private set; } = 10;
        
        public void SetDroneCount(int count)
        {
            _droneCount.Value = Math.Clamp(count, 0, MaxDroneCount);
        }
        
        public void SetDroneSpeed(float speed)
        {
            _droneSpeed.Value = Math.Clamp(speed, 0, MaxDroneSpeed);
        }
        
        public void Dispose()
        {
            _droneCount?.Dispose();
            _droneSpeed?.Dispose();
        }
    }
}