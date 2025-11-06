using System.Collections.Generic;
using Components;
using DroneFactory.Interfaces;
using R3;
using UI;
using UnityEngine;
using Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace DroneFactory
{
    public class DroneSpawner : MonoBehaviour, IDroneSpawner
    {
        private IDronSpawnFactory _droneSpawnFactory;
        
        [SerializeField] private Transform _teamBlueSpawnPoint; 
        [SerializeField] private Transform _teamRedSpawnPoint;
        
        private readonly Vector2 _randomOffsetSpawn = new(-4f, 4f);
        private DroneSimulationModel _model;

        private Dictionary<EDroneFraction, List<DroneView>> _countDronesInTeam;

        [Inject]
        public void Construct(IDronSpawnFactory droneSpawnFactory, DroneSimulationModel model)
        {
            _droneSpawnFactory = droneSpawnFactory;
            _model = model;
        }

        private void OnEnable()
        {
            _model.DroneCount.Subscribe(OnDroneCountChanged).AddTo(this);
        }
        
        private void Start()
        {
            _countDronesInTeam = new Dictionary<EDroneFraction, List<DroneView>>
            {
                {EDroneFraction.Red, new List<DroneView>()},
                {EDroneFraction.Blue, new List<DroneView>()}
            };
        }
        
        private void SpawnDroneTeam(EDroneFraction type, Transform spawnPoint, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var randomX = Random.Range(_randomOffsetSpawn.x, _randomOffsetSpawn.y);
                var randomOffset = new Vector3(randomX, 0f, 0f);
                var drone = _droneSpawnFactory.CreateDrone(type, randomOffset + spawnPoint.position);
                _countDronesInTeam[type].Add(drone);
            }
        }
        
        private void OnDroneCountChanged(int count)
        {
            UpdateTeamDroneCount(EDroneFraction.Blue, count);
            UpdateTeamDroneCount(EDroneFraction.Red, count);
        }
        
        private void UpdateTeamDroneCount(EDroneFraction team, int newCount)
        {
            var currentCount = _countDronesInTeam[team].Count;
            var difference = newCount - currentCount;

            if (difference > 0)
            {
                var spawnPoint = team == EDroneFraction.Blue
                    ? _teamBlueSpawnPoint 
                    : _teamRedSpawnPoint;
                
                SpawnDroneTeam(team, spawnPoint, difference);
            }
            else if (difference < 0)
            {
                RemoveDrones(team, Mathf.Abs(difference));
            }
        }
        
        private void RemoveDrones(EDroneFraction team, int count)
        {
            var teamList = _countDronesInTeam[team];
            var removeCount = Mathf.Min(count, teamList.Count);

            for (var i = 0; i < removeCount; i++)
            {
                var index = teamList.Count - 1;
                var drone = teamList[index];
                
                _droneSpawnFactory.ReturnToPool(team, drone);
                teamList.RemoveAt(index);
            }
        }
    }
}
