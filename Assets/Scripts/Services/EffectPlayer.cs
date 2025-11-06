using UnityEngine;

namespace Services
{
    public class EffectPlayer : IEffectPlayer
    {
        public void PlayUnloadEffect(Vector3 position)
        {
            Debug.Log($"[Effect] Unload at {position}");
        }
    }
}
