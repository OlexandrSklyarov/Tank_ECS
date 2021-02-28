using UnityEngine;

namespace SA.Tanks.FSM
{
    [CreateAssetMenu(menuName = "Data/FSM/Decisions/LookForwardDecision")]
    public class LookForwardDecision : DecisionFSM
    {
        public override bool Decide(ref BrainAIComponent brain)
        {
            return Look(ref brain);
        }


        bool Look(ref BrainAIComponent brain)
        {        
            #if DEBUG
            Debug.DrawRay(  brain.Eyes.position, 
                            brain.Eyes.forward * brain.EnemyStats.LookRange, 
                            Color.green);
            #endif

            if (Physics.SphereCast( brain.Eyes.position,
                                    brain.EnemyStats.LookSphereCastRadius,
                                    brain.Eyes.forward,
                                    out RaycastHit hit,
                                    brain.EnemyStats.LookRange,
                                    brain.PlayerLayer))
            {
                brain.ChaseTarget = hit.transform;
                return true;
            }      

            return false;                  
        } 
    }
}