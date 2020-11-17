using UnityEngine;

namespace SA.Tanks.FSM
{
    [CreateAssetMenu(menuName = "Data/FSM/Decisions/TargetAliveDecision")]
    public class TargetAliveDecision : DecisionFSM
    {
        public override bool Decide(ref BrainAIComponent brain)
        {
            return brain.ChaseTarget.gameObject.activeSelf;
        }
    }
}