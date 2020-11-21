using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks.FSM
{
    [CreateAssetMenu(menuName = "Data/FSM/Action/Scan")]
    public class ScanAction : ActionFSM
    {
        public override void Act(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            Scan(ref brain, ref entity);
        }


        void Scan(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            if(brain.Agent.enabled)
            {
                brain.Agent.isStopped = true;
                brain.Agent.enabled = false;
            }              

            var turnSpeed = brain.EnemyStats.SearchingTurnSpeed * Time.deltaTime;
            
            var rb = entity.Get<MoveComponent>().RB;
            rb.transform.Rotate(new Vector3(0f, rb.transform.rotation.y + turnSpeed, 0f));
        }
    }
}