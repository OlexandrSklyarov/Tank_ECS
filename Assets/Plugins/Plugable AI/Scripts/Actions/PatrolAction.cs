using UnityEngine;


[CreateAssetMenu(menuName = "PlugableAI/Action/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }


    void Patrol(StateController controller)
    {
        var agent = controller.navMeshAgent;

        agent.destination = 
            controller
            .waitpoints[controller.nextWayPoint]
            .position;

        agent.Resume();    

        var distToPoint = (agent.destination - agent.transform.position).magnitude;

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.waitpoints.Length;
            Debug.Log("Set new waypoint");
        }
    }
    
}