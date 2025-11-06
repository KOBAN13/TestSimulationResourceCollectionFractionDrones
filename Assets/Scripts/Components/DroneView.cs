using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Components
{
    public class DroneView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EDroneFraction _fraction;

        public NavMeshAgent Agent => _navMeshAgent;
        public EDroneFraction Fraction => _fraction;
    }
}