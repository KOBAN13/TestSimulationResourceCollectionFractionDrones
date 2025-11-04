using Components;
using UnityEngine;

namespace ResourceFactory.Interfaces
{
    public interface IResourceFactory
    {
        ResourceView CreateResource(Vector3 position);
        void ReleaseResource(ResourceView enemy);
    }
}