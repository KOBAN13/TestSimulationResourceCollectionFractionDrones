using System.Collections.Generic;
using System.Linq;
using Components;
using ResourceFactory.Interfaces;
using UnityEngine;

namespace Services
{
    public class ResourceDirectory : IResourceDirectory
    {
        private readonly HashSet<ResourceView> _all = new();
        private readonly HashSet<ResourceView> _reserved = new();
        private readonly IResourceSpawnFactory _resourceSpawnFactory;

        public ResourceDirectory(IResourceSpawnFactory resourceSpawnFactory)
        {
            _resourceSpawnFactory = resourceSpawnFactory;
        }

        public IReadOnlyCollection<ResourceView> AllResources => _all;

        public void Register(ResourceView resource)
        {
            if (resource != null)
                _all.Add(resource);
        }

        public void Unregister(ResourceView resource)
        {
            if (resource == null)
                return;
            
            _reserved.Remove(resource);
            _all.Remove(resource);
        }

        public bool TryReserveNearest(Vector3 fromPosition, out ResourceView resource)
        {
            resource = GetNearestFreeResource(fromPosition);

            if (resource == null)
                return false;

            _reserved.Add(resource);
            resource.IsReserved = true;
            return true;
        }

        public ResourceView GetNearestFreeResource(Vector3 fromPosition)
        {
            return _all
                .Where(resource => resource != null && !_reserved.Contains(resource) && !resource.IsReserved)
                .OrderBy(r => Vector3.SqrMagnitude(r.transform.position - fromPosition))
                .FirstOrDefault();
        }

        public void Release(ResourceView resource)
        {
            if (resource == null) return;
            _reserved.Remove(resource);
            resource.IsReserved = false;
        }

        public void Consume(ResourceView resource)
        {
            if (resource == null) 
                return;
            
            _reserved.Remove(resource);
            resource.IsReserved = false;
            
            _all.Remove(resource);
            
            if (resource != null)
            {
                _resourceSpawnFactory.ReleaseResource(resource);
            }
        }
    }
}
