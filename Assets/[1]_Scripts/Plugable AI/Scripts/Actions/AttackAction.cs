using UnityEngine;
using Leopotam.Ecs;
using SA.Tanks;

[CreateAssetMenu(menuName = "PlugableAI/Action/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }


    void Attack(StateController controller)
    {
        #if DEBUG_GIZMO
        Debug.DrawRay(  controller.eyes.position, 
                        controller.eyes.forward * controller.enemyStats.AttackRange, 
                        Color.red);
        #endif

        if (Physics.SphereCast( controller.eyes.position,
                                controller.enemyStats.LookSphereCastRadius,
                                controller.eyes.forward,
                                out RaycastHit hit,
                                controller.enemyStats.AttackRange,
                                controller.playerLayer))
        {
            if (controller.IsCheckCountDownElapsed(controller.enemyStats.AttackRate))
            {    
                Fire(controller);              
            }            
        }       
    }


    private void Fire(StateController controller)
    {
        controller.Entity.Replace(new ShootingEvent());
        Debug.Log("Fire action!!!ssssssss");  
    }
}