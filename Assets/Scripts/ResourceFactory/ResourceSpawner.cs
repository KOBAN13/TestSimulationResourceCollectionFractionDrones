using System;
using R3;
using ResourceFactory.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Services;
using UI;

namespace ResourceFactory
{
    public class ResourceSpawner : MonoBehaviour, IResourceSpawner
    {
        [SerializeField] private float _spawnRadius = 10f;

        private IResourceSpawnFactory _resourceSpawnFactory;
        private IResourceDirectory _resourceDirectory;
        
        private DroneSimulationModel _model;
        
        private IDisposable _spawnDisposable;

        [Inject]
        private void Construct(DroneSimulationModel model, IResourceSpawnFactory resourceSpawnFactory, IResourceDirectory resourceDirectory)
        {
            _model = model;
            _resourceSpawnFactory = resourceSpawnFactory;
            _resourceDirectory = resourceDirectory;
        }

        private void Awake()
        {
            _model.ResourceSpawnGeneration
                .Subscribe(StartSpawn)
                .AddTo(this);
        }

        private void OnDestroy()
        {
            _spawnDisposable?.Dispose();
        }

        private void StartSpawn(float spawnDelay)
        {
            _spawnDisposable?.Dispose();
            
            _spawnDisposable = Observable
                .Timer(TimeSpan.FromSeconds(spawnDelay), TimeSpan.FromSeconds(spawnDelay))
                .ObserveOnMainThread()
                .Subscribe(_ => SpawnResource());
        }

        private void SpawnResource()
        {
            var spawnPoint = GetRandomPointInRadius();
            var resource = _resourceSpawnFactory.CreateResource(spawnPoint);
            _resourceDirectory?.Register(resource);
        }

        private Vector3 GetRandomPointInRadius()
        {
            var randomCircle = Random.insideUnitCircle * _spawnRadius;
            var point = new Vector3(randomCircle.x, 0f, randomCircle.y);
            return transform.position + point;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
            
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2f, $"Spawn Radius: {_spawnRadius}");
        }
#endif
    }
}