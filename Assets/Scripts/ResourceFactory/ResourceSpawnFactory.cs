using Components;
using Pool;
using ResourceFactory.Interfaces;
using UnityEngine;
using Zenject;

namespace ResourceFactory
{
    public class ResourceSpawnFactory : IResourceFactory, IInitializable
    {
        private IGenericObjectPool<ResourceView> _objectFactory;
        private IResourceSpawnData _spawnData;
        
        [Inject] 
        private void Construct(IGenericObjectPool<ResourceView> objectFactory, IResourceSpawnData spawnData)
        {
            _objectFactory = objectFactory;
            _spawnData = spawnData;
        }

        public void Initialize()
        {
            _objectFactory.Initialize(_spawnData.ResourcePrefab);
        }
        
        public ResourceView CreateResource(Vector3 position)
        {
            var resource = _objectFactory.GetObject();
            resource.transform.position = position;
            return resource;
        }
        
        public void ReleaseResource(ResourceView resource) => _objectFactory.ReturnObject(resource);
    }
}