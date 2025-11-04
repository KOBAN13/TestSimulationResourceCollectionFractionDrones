using System;
using Components;
using Factory.Utils;

namespace DroneFactory
{
    [Serializable]
    public class DroneSpawnSettings
    {
        public EDroneFraction droneFraction;
        public DroneView droneViewPrefab;
    }
}