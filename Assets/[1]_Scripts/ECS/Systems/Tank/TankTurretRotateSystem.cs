using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct TankTurretRotateSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<TankTurretComponent, MoveComponent, AimingComponent> turretFilter;

        const float MAX_DISTANCE_TO_TARGET = 4f;

        #endregion


        #region Run

        public void Run()
        {
            foreach(var id in turretFilter)
            {
                var turret = turretFilter.Get1(id);
                var tankRB = turretFilter.Get2(id).RB;
                var aiming = turretFilter.Get3(id);

                //назначаем поворот по умолчанию, как у танка
                var targetRotate = tankRB.rotation;

                //получаем поворот к цели, если она есть и она не слишком близко
                if (aiming.IsTargetExist &&
                    Vector3.Distance(turret.Target, turret.TurretTransform.position) > MAX_DISTANCE_TO_TARGET)
                {
                    var direction = aiming.AimPosition - turret.TurretTransform.position;                   
                    targetRotate = Quaternion.LookRotation(direction);
                }                 

                var rot = Quaternion.Lerp(  turret.TurretTransform.rotation, targetRotate, turret.RotateSpeed * Time.deltaTime);               
                //поворачиваем турель
                turret.TurretTransform.rotation = rot;
            }
        }

        #endregion

    }
}
