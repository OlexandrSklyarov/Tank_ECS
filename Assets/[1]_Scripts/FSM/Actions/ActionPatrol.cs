using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks.FSM
{
    [CreateAssetMenu(menuName = "Data/FSM/Action/Patrol")]
    public class ActionPatrol : ActionFSM
    {
        public override void Act(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            Patrol(ref brain, ref entity);
        }


        void Patrol(ref BrainAIComponent brain, ref EcsEntity entity)
        {            
            var agent = brain.Agent;

            agent.enabled = true;

            agent.destination = 
                brain
                .Waitpoints[brain.NextWayPoint]
                .position;

            var distToPoint = (agent.destination - agent.transform.position).magnitude;

            if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                brain.NextWayPoint = (brain.NextWayPoint + 1) % brain.Waitpoints.Length;
            }
        }
        
    }
}