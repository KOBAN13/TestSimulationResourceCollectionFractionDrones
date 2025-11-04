using Components;
using DroneFactory;
using DroneFactory.Data;
using Pool;
using ResourceFactory;
using ResourceFactory.Data;
using States;
using UnityEngine;
using Zenject;

namespace Di
{
    public class BootstrupContext : MonoInstaller
    {
        [SerializeField] private DroneSpawnData _droneSpawnSettings;
        [SerializeField] private ResourceSpawnData _resourceSpawnSettings;
        
        [SerializeField] private DroneSpawner _droneSpawner;
        [SerializeField] private ResourceSpawner _resourceSpawner;
        
        public override void InstallBindings()
        {
            BindStateMachine();
            BuildSpawners();
        }
            
        private void BuildSpawners()
        {
            Container.BindInterfacesAndSelfTo<GenericObjectPool<DroneView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<GenericObjectPool<ResourceView>>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<DroneSpawnData>().FromScriptableObject(_droneSpawnSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawnData>().FromScriptableObject(_resourceSpawnSettings).AsSingle();
            
            Container.BindInterfacesAndSelfTo<DroneSpawnFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawnFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<DroneSpawner>().FromInstance(_droneSpawner).AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawner>().FromInstance(_resourceSpawner).AsSingle();
        }

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<DronStateMachine>().AsSingle();
        }
    }
}