using Components;
using ResourceFactory.Interfaces;
using UnityEngine;

namespace ResourceFactory.Data
{
    [CreateAssetMenu(fileName = nameof(ResourceSpawnData), menuName = nameof(ResourceSpawnData))]
    public class ResourceSpawnData : ScriptableObject, IResourceSpawnData
    {
        [field: SerializeField] public ResourceView ResourcePrefab { get; private set; }
    }
}