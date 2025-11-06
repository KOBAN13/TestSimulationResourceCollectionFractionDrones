using System.Collections.Generic;
using DroneFactory;
using DroneFactory.Data;
using ResourceFactory;
using ResourceFactory.Data;
using Services;
using UnityEngine;
using Utils;
using Utils.SerializedDictionary;
using Zenject;

namespace Di
{
    public class BootstrupContext : MonoInstaller
    {
        [SerializeField] private DroneSpawnData _droneSpawnSettings;
        [SerializeField] private ResourceSpawnData _resourceSpawnSettings;
        
        [SerializeField] private DroneSpawner _droneSpawner;
        [SerializeField] private ResourceSpawner _resourceSpawner;
        
        [SerializeField] private DroneBaseDictionary _droneBase;
        
        public override void InstallBindings()
        {
            BindServices();
            BuildSpawners();
        }
            
        private void BuildSpawners()
        {
            Container.BindInterfacesAndSelfTo<DroneSpawnData>().FromScriptableObject(_droneSpawnSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawnData>().FromScriptableObject(_resourceSpawnSettings).AsSingle();
            
            Container.BindInterfacesAndSelfTo<DroneSpawnSpawnFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawnFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<DroneSpawner>().FromInstance(_droneSpawner).AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceSpawner>().FromInstance(_resourceSpawner).AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<DroneBaseDictionary>().FromInstance(_droneBase).AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceDirectory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EffectPlayer>().AsSingle();
        }
    }
}