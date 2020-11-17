using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks.FSM
{
    [CreateAssetMenu(menuName = "Data/FSM/Action/Chase")]
    public class ActionChase: ActionFSM
    {
        public override void Act(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            Chase(ref brain, ref entity);
        }


        void Chase(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            brain.Agent.destination = brain.ChaseTarget.position;
            brain.Agent.Resume();
        }
    }
}