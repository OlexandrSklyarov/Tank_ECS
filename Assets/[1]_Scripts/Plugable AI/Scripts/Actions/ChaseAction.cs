using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PlugableAI/Action/ChaseAction")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }


    void Chase(StateController controller)
    {
        controller.NavMeshAgent.destination = controller.chaseTarget.position;
        controller.NavMeshAgent.Resume();
    }
}