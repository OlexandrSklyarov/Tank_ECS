using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlugableAI/Decisions/Scan")]
public class ScanDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return Scan(controller);
    }


    bool Scan(StateController controller)
    {        
        controller.NavMeshAgent.isStopped = true;
        var turnSpeed = controller.enemyStats.SearchingTurnSpeed * Time.deltaTime;
        controller.transform.Rotate(0f, turnSpeed, 0f);

        return controller.IsCheckCountDownElapsed(controller.enemyStats.SearchDuration);         
    } 
    
}