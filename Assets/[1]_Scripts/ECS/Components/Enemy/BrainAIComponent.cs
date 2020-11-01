using UnityEngine.AI;

namespace SA.Tanks
{
    public struct BrainAIComponent
    {
        public NavMeshAgent Agent {get; set;}
        public StateController StateController {get; set;}
    }
}