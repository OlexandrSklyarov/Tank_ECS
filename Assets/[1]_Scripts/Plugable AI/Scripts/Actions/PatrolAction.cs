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
        var agent = controller.NavMeshAgent;

        agent.destination = 
            controller
            .Waitpoints[controller.nextWayPoint]
            .position;

        agent.Resume();    

        var distToPoint = (agent.destination - agent.transform.position).magnitude;

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.Waitpoints.Length;
            Debug.Log("Set new waypoint");
        }
    }
    
}