using Components;
using Pool;
using ResourceFactory.Interfaces;
using UnityEngine;
using Zenject;

namespace ResourceFactory
{
    public class ResourceSpawnSpawnFactory : IResourceSpawnFactory, IInitializable
    {
        private IGenericObjectPool<ResourceView> _objectFactory;
        private IResourceSpawnData _spawnData;
        private DiContainer _container;
        
        [Inject] 
        private void Construct(IResourceSpawnData spawnData, DiContainer container)
        {
            _spawnData = spawnData;
            _container = container;
        }

        public void Initialize()
        {
            _objectFactory = new GenericObjectPool<ResourceView>(_container);
            _objectFactory.Initialize(_spawnData.ResourcePrefab);
        }
        
        public ResourceView CreateResource(Vector3 position)
        {
            var resource = _objectFactory.GetObject();
            resource.transform.position = position;
            return resource;
        }
        
        public void ReleaseResource(ResourceView resource)
            => _objectFactory.ReturnObject(resource);
    }
}