using Components;
using UnityEngine;
using UnityEngine.AI;

namespace Utils
{
    public class DroneContext
    {
        public DroneView View { get; }
        public NavMeshAgent Agent { get; }
        public Transform BaseTransform { get; }
        public EDroneFraction Fraction { get; }
        public ResourceView TargetResource { get; set; }
        public bool HasCargo { get; set; }

        public Vector3 Position => View.transform.position;

        public DroneContext(DroneView view, NavMeshAgent agent, Transform baseTransform, EDroneFraction fraction)
        {
            View = view;
            Agent = agent;
            BaseTransform = baseTransform;
            Fraction = fraction;
        }
    }
}
