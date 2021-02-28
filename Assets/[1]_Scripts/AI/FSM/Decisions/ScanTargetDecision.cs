using UnityEngine;

namespace SA.Tanks.FSM
{
    [CreateAssetMenu(menuName = "Data/FSM/Decisions/ScanTargetDecision")]
    public class ScanTargetDecision : DecisionFSM
    {
        public override bool Decide(ref BrainAIComponent brain)
        {
            return Scan(ref brain);
        }


        bool Scan(ref BrainAIComponent brain)
        {   
            return brain.IsCheckCountDownElapsed(brain.EnemyStats.SearchDuration);         
        }
    }
}