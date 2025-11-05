using UnityEngine;
using UnityEngine.AI;

namespace Components
{
    public class DroneView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _baseTransform;
        [SerializeField] private Utils.EDroneFraction _fraction;

        public NavMeshAgent Agent => _navMeshAgent;
        public Transform BaseTransform => _baseTransform;
        public Utils.EDroneFraction Fraction => _fraction;
    }
}