using UnityEngine;


[CreateAssetMenu(menuName = "PlugableAI/Action/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }


    void Attack(StateController controller)
    {
        #if DEBUG
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
                controller.tankShooting?.Fire(controller.enemyStats.AttackForce,
                                             controller.enemyStats.AttackRate);
                
                Debug.Log("Fire action!!!ssssssss");
            }            
        }       
    }
}