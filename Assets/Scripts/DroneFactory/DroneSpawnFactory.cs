using Components;
using DroneFactory.Interfaces;
using Factory.Pool;
using Zenject;

namespace DroneFactory
{
    public class DroneSpawnFactory : IDronFactory
    {
        private IGenericObjectPool<DroneView> _objectFactory;
        
        [Inject] 
        private void Construct(IGenericObjectPool<DroneView> objectFactory) => 
            _objectFactory = objectFactory;

        public DroneView CreateDrone() => _objectFactory.GetObject();
        
        public void ReturnToPool(DroneView enemy) => _objectFactory.ReturnObject(enemy);
    }
}