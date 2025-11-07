using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Components
{
    public class DroneView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EDroneFraction _fraction;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private ParticleSystem _particleSystem;

        public NavMeshAgent Agent => _navMeshAgent;
        public EDroneFraction Fraction => _fraction;
        public LineRenderer LineRenderer => _lineRenderer;
        public ParticleSystem ParticleSystem => _particleSystem;
    }
}