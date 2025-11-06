using System.Collections.Generic;
using Components;
using UnityEngine;

namespace Services
{
    public interface IResourceDirectory
    {
        void Register(ResourceView resource);
        void Unregister(ResourceView resource);
        bool TryReserveNearest(Vector3 fromPosition, out ResourceView resource);
        ResourceView GetNearestFreeResource(Vector3 fromPosition);
        void Release(ResourceView resource);
        void Consume(ResourceView resource);
        IReadOnlyCollection<ResourceView> AllResources { get; }
    }
}
