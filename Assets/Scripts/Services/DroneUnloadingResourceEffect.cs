using System;
using UnityEngine;

namespace Services
{
    public class DroneUnloadingResourceEffect : IDroneUnloadingResourceEffect
    {
        private readonly ParticleSystem _particleSystem;
        private readonly float _timeToPlayEffect = 2f;

        public DroneUnloadingResourceEffect(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }

        public async Awaitable PlayUnloadEffect(Vector3 position)
        {
            _particleSystem.Play();
            
            await Awaitable.WaitForSecondsAsync(_timeToPlayEffect);

            _particleSystem.Stop();
        }
    }
}