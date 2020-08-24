using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct AimingSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<InputEventComponent, TankTurretComponent> palyerAimingFilter;

        #endregion


        public void Run()
        {
            PlayerAiming();
        }


        void PlayerAiming()
        {
            foreach (var id in palyerAimingFilter)
            {
                ref var input = ref palyerAimingFilter.Get1(id);
                ref var turret = ref palyerAimingFilter.Get2(id);

                turret.Target = (!input.AimPosition.Equals(Vector3.zero)) ?
                    input.AimPosition : Vector3.zero;
            }
        }
    }
}
