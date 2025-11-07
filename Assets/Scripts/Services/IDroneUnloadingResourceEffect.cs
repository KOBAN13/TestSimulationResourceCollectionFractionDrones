using UnityEngine;

namespace Services
{
    public interface IDroneUnloadingResourceEffect
    {
        Awaitable PlayUnloadEffect(Vector3 position);
    }
}
