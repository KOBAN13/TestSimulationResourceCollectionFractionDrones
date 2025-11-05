using System;
using Components;
using Utils;

namespace DroneFactory
{
    [Serializable]
    public class DroneSpawnSettings
    {
        public EDroneFraction droneFraction;
        public DroneView droneViewPrefab;
    }
}