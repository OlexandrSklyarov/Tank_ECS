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
            brain.Agent.isStopped = true;
            var turnSpeed = brain.EnemyStats.SearchingTurnSpeed * Time.deltaTime;

            entity.Replace(new InputMoveDirectionEvent()
            {
                Vertical = 1f
            });
        }
    }
}