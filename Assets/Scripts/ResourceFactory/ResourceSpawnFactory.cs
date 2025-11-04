using Components;
using Factory.Pool;
using ResourceFactory.Interfaces;
using UnityEngine;
using Zenject;

namespace ResourceFactory
{
    public class ResourceSpawnFactory : IResourceFactory
    {
        private IGenericObjectPool<ResourceView> _objectFactory;
        
        [Inject] 
        private void Construct(IGenericObjectPool<ResourceView> objectFactory) => 
            _objectFactory = objectFactory;

        public ResourceView CreateResource(Vector3 position) => _objectFactory.GetObject();
        
        public void ReleaseResource(ResourceView enemy) => _objectFactory.ReturnObject(enemy);
    }
}