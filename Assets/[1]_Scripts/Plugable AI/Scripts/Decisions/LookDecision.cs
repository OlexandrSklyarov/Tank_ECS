using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlugableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return Look(controller);
    }


    bool Look(StateController controller)
    {        
        #if DEBUG
        Debug.DrawRay(  controller.eyes.position, 
                        controller.eyes.forward * controller.enemyStats.LookRange, 
                        Color.green);
        #endif

        if (Physics.SphereCast( controller.eyes.position,
                                controller.enemyStats.LookSphereCastRadius,
                                controller.eyes.forward,
                                out RaycastHit hit,
                                controller.enemyStats.LookRange,
                                controller.playerLayer))
        {
            controller.chaseTarget = hit.transform;
            return true;
        }      

        return false;                  
    } 
    
}