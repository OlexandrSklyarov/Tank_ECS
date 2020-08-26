using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct AimingSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<AimingComponent, TankTurretComponent> aimingFilter;

        #endregion


        public void Run()
        {
            PlayerAiming();
        }


        void PlayerAiming()
        {
            foreach (var id in aimingFilter)
            {
                ref var aiming = ref aimingFilter.Get1(id);
                ref var turret = ref aimingFilter.Get2(id);

                turret.Target = (!aiming.AimPosition.Equals(Vector3.zero)) ?
                    aiming.AimPosition : Vector3.zero;
            }
        }
    }
}
