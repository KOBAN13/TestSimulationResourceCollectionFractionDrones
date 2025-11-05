using UnityEngine;

namespace Services
{
    public class EffectPlayer : IEffectPlayer
    {
        public void PlayUnloadEffect(Vector3 position)
        {
            // Placeholder: replace with particle system or animation trigger.
            Debug.Log($"[Effect] Unload at {position}");
        }
    }
}
