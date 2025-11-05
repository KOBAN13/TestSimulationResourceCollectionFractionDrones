using DroneFactory.Interfaces;
using UnityEngine;
using Utils;
using Zenject;

namespace DroneFactory
{
    public class DroneSpawner : MonoBehaviour, IDroneSpawner
    {
        private IDronSpawnFactory _droneSpawnFactory;
        
        [SerializeField] private Transform _teamBlueSpawnPoint; 
        [SerializeField] private Transform _teamRedSpawnPoint;

        [SerializeField, Min(0)] private int _teamRedCount;
        [SerializeField, Min(0)] private int _teamBlueCount;

        [Inject]
        public void Construct(IDronSpawnFactory droneSpawnFactory)
        {
            _droneSpawnFactory = droneSpawnFactory;
        }
        
        private void Start()
        {
            SpawnDrones();
        }

        private void SpawnDrones()
        {
            SpawnDroneTeam(EDroneFraction.Blue, _teamBlueSpawnPoint, _teamBlueCount);
            SpawnDroneTeam(EDroneFraction.Red, _teamRedSpawnPoint, _teamRedCount);
        }

        private void SpawnDroneTeam(EDroneFraction type, Transform spawnPoint, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var randomOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                _droneSpawnFactory.CreateDrone(type, randomOffset + spawnPoint.position);
            }
        }
        
        public void SetTeamACount(int count) => _teamRedCount = count;
        public void SetTeamBCount(int count) => _teamBlueCount = count;
    }
}