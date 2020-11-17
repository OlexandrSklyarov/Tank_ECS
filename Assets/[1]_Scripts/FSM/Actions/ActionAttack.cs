using Leopotam.Ecs;
using UnityEngine;


namespace SA.Tanks.FSM
{
    [CreateAssetMenu(menuName = "Data/FSM/Action/Attack")]
    public class ActionAttack : ActionFSM
    {
        public override void Act(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            Attack(ref brain, ref entity);
        }


        void Attack(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            #if DEBUG_GIZMO
            Debug.DrawRay(  brain.Eyes.position, 
                            brain.Eyes.forward * brain.EnemyStats.AttackRange, 
                            Color.red);
            #endif

            if (Physics.SphereCast( brain.Eyes.position,
                                    brain.EnemyStats.LookSphereCastRadius,
                                    brain.Eyes.forward,
                                    out RaycastHit hit,
                                    brain.EnemyStats.AttackRange,
                                    brain.PlayerLayer))
            {
                if (brain.IsCheckCountDownElapsed(brain.EnemyStats.AttackRate))
                {    
                    Fire(ref entity);              
                }            
            }       
        }


        private void Fire(ref EcsEntity entity)
        {
            entity.Replace(new ShootingEvent());
            Debug.Log("Fire action!!!");  
        }
    }
}