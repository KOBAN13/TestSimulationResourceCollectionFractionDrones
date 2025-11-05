using System.Collections.Generic;
using System.Linq;
using Components;
using UnityEngine;

namespace Services
{
    public class ResourceDirectory : IResourceDirectory
    {
        private readonly HashSet<ResourceView> _all = new();
        private readonly HashSet<ResourceView> _reserved = new();
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
            resource = _all
                .Where(r => r != null && !_reserved.Contains(r))
                .OrderBy(r => Vector3.SqrMagnitude(r.transform.position - fromPosition))
                .FirstOrDefault();

            if (resource == null)
                return false;

            _reserved.Add(resource);
            return true;
        }

        public void Release(ResourceView resource)
        {
            if (resource == null) return;
            _reserved.Remove(resource);
        }

        public void Consume(ResourceView resource)
        {
            if (resource == null) 
                return;
            
            _reserved.Remove(resource);
            
            _all.Remove(resource);
            
            if (resource != null)
            {
                Object.Destroy(resource.gameObject);
            }
        }
    }
}
