using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using DroneFactory.Interfaces;
using Pool;
using UnityEngine;
using Utils;
using Zenject;

namespace DroneFactory
{
    public class DroneSpawnSpawnFactory : IDronSpawnFactory, IInitializable
    {
        private readonly Dictionary<EDroneFraction, IGenericObjectPool<DroneView>> _pools = new();
        private IDronSpawnData _spawnData;

        [Inject]
        private void Construct(IDronSpawnData spawnData)
        {
            _spawnData = spawnData;
        }

        public void Initialize()
        {
            foreach (EDroneFraction type in Enum.GetValues(typeof(EDroneFraction)))
            {
                if (type == EDroneFraction.None)
                    continue;
                
                var config = _spawnData.SpawnSettings.FirstOrDefault(s => s.droneFraction == type);

                if (config == null)
                {
                    Debug.LogError($"[DroneFactory] Нет конфига для типа {type}");
                    continue;
                }

                var pool = new GenericObjectPool<DroneView>();
                pool.Initialize(config.droneViewPrefab);

                _pools[type] = pool;
            }
        }

        public DroneView CreateDrone(EDroneFraction type, Vector3 position)
        {
            if (!_pools.TryGetValue(type, out var pool))
                return null;

            var drone = pool.GetObject();
            drone.transform.position = position;
            return drone;
        }

        public void ReturnToPool(EDroneFraction type, DroneView drone)
        {
            _pools[type].ReturnObject(drone);
        }
    }
}