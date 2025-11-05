using Components;
using DroneFactory;
using DroneFactory.Data;
using Pool;
using ResourceFactory;
using ResourceFactory.Data;
using Services;
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
            BindServices();
            BuildSpawners();
        }
            
        private void BuildSpawners()
        {
            Container.BindInterfacesAndSelfTo<GenericObjectPool<ResourceView>>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<DroneSpawnData>().FromScriptableObject(_droneSpawnSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawnData>().FromScriptableObject(_resourceSpawnSettings).AsSingle();
            
            Container.BindInterfacesAndSelfTo<DroneSpawnSpawnFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawnFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<DroneSpawner>().FromInstance(_droneSpawner).AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawner>().FromInstance(_resourceSpawner).AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<IResourceDirectory>().To<ResourceDirectory>().AsSingle();
            Container.Bind<IEffectPlayer>().To<EffectPlayer>().AsSingle();
        }
    }
}