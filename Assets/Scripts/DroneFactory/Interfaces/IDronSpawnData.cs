using System.Collections.Generic;

namespace DroneFactory.Interfaces
{
    public interface IDronSpawnData
    {
        IReadOnlyList<DroneSpawnSettings> SpawnSettings { get; }
    }
}