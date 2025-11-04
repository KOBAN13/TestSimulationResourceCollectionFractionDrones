using System.Collections.Generic;
using DroneFactory.Interfaces;
using UnityEngine;

namespace DroneFactory.Data
{
    [CreateAssetMenu(fileName = nameof(DroneSpawnData), menuName = nameof(DroneSpawnData))]
    public class DroneSpawnData : ScriptableObject, IDronSpawnData
    {
        [SerializeField] private List<DroneSpawnSettings> _spawnSettings;
        
        public IReadOnlyList<DroneSpawnSettings> SpawnSettings => _spawnSettings;
    }
}