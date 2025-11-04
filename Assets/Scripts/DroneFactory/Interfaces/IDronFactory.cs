using Components;

namespace DroneFactory.Interfaces
{
    public interface IDronFactory
    {
        DroneView CreateDrone();
        void ReturnToPool(DroneView enemy);
    }
}