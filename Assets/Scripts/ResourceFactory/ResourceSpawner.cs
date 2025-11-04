using System;
using System.Collections;
using R3;
using ResourceFactory.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ResourceFactory
{
    public class ResourceSpawner : MonoBehaviour, IResourceSpawner
    {
        [SerializeField] private float _spawnRadius = 10f;

        private IResourceFactory _resourceFactory;
        private float _spawnDeleay = 3f; //TODO 
        private IDisposable _spawnDisposable;

        [Inject]
        private void Construct(IResourceFactory resourceFactory) =>
            _resourceFactory = resourceFactory;

        private void Start()
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            _spawnDisposable = Observable.Timer(TimeSpan.FromSeconds(_spawnDeleay))
                .Subscribe(_ => SpawnResource());
        }

        private void SpawnResource()
        {
            var spawnPoint = GetRandomPointInRadius();
            var resource = _resourceFactory.CreateResource(spawnPoint);
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

            // Подсказка в редакторе
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2f, $"Spawn Radius: {_spawnRadius}");
        }
#endif
    }
}