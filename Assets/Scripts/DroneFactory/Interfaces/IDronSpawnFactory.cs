using Components;
using UnityEngine;
using Utils;

namespace DroneFactory.Interfaces
{
    public interface IDronSpawnFactory
    {
        DroneView CreateDrone(EDroneFraction type, Vector3 position);
        void ReturnToPool(EDroneFraction type, DroneView enemy);
    }
}