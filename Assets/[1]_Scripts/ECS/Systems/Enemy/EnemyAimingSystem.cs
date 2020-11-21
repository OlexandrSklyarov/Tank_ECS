using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct  EnemyAimingSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<AimingComponent, BrainAIComponent> aimingFilter;

        #endregion


        public void Run()
        {       
            foreach (var id in aimingFilter)
            {
                ref var aiming = ref aimingFilter.Get1(id);
                aiming.AimPosition = Vector3.zero;

                var brain = aimingFilter.Get2(id);
                var target = brain.ChaseTarget;
                var eyes = brain.Eyes;
                var lookDist = brain.EnemyStats.LookSphereCastRadius;

                //если цель есть, и она в радуусе видимости
                if (target && Vector3.Distance(eyes.position, target.position) < lookDist)
                {
                    aiming.AimPosition = target.position + Vector3.up;
                }
            }
        }
    }
}