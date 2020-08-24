using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct TankTurretRotateSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<TankTurretComponent, MoveComponent> turretFilter;

        const float MAX_DISTANCE_TO_TARGET = 4f;

        #endregion


        #region Run

        public void Run()
        {
            foreach(var id in turretFilter)
            {
                var turret = turretFilter.Get1(id);
                var tankRotation = turretFilter.Get2(id).RB.rotation;

                //назначаем поворот по умолчанию, как у танка
                var targetRotate = tankRotation;

                //получаем поворот к цели, если она есть и она не слишком близко
                if (!turret.Target.Equals(Vector3.zero)
                    && Vector3.Distance(turret.Target, turret.TurretTransform.position) > MAX_DISTANCE_TO_TARGET)
                {
                    var direction = turret.Target - turret.TurretTransform.position;
                    targetRotate = Quaternion.LookRotation(direction);
                }

                //поворачиваем турель
                turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, 
                                                                targetRotate,
                                                                turret.RotateSpeed * Time.deltaTime);
            }
        }

        #endregion

    }
}
