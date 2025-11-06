using Components;
using UnityEngine;

namespace ResourceFactory.Interfaces
{
    public interface IResourceSpawnFactory
    {
        ResourceView CreateResource(Vector3 position);
        void ReleaseResource(ResourceView resource);
    }
}