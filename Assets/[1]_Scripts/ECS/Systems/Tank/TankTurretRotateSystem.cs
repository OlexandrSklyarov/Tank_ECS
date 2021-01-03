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
                var newTurretRotation = tankRB.rotation;
                //var newBarrelRotation = tankRB.rotation;

                //получаем поворот к цели, если она есть и она не слишком близко
                if (aiming.IsTargetExist &&
                    Vector3.Distance(turret.Target, turret.TurretTransform.position) > MAX_DISTANCE_TO_TARGET)
                {
                    //поворачиваем башню вокруг вертикальной оси танка (локально) 
                    var direction = aiming.AimPosition - turret.TurretTransform.position;
                    TurretRotation(direction, ref newTurretRotation);
                    
                    //поаорот ствола танка
                    // direction = aiming.AimPosition - turret.BarrelTransform.position;
                    // BarrelRotation(direction, ref newBarrelRotation);                      
                }                               

                var speed = turret.RotateSpeed * Time.deltaTime;

                //поворачиваем турель
                turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, newTurretRotation, speed);

                //поворачиваем дуло
                // turret.BarrelTransform.rotation = Quaternion.Lerp(turret.BarrelTransform.rotation, newBarrelRotation, speed);

            }
        }


        void TurretRotation(Vector3 direction, ref Quaternion newTurretRotation)
        {                                                  
            newTurretRotation = Quaternion.LookRotation(direction);
            var tempRot = newTurretRotation.eulerAngles;
            tempRot.x = 0f;
            newTurretRotation = Quaternion.Euler(tempRot);
        }


        void BarrelRotation(Vector3 direction, ref Quaternion newBarrelRotation)
        {                                                  
            newBarrelRotation = Quaternion.LookRotation(direction);
            var tempRot = newBarrelRotation.eulerAngles;
            tempRot.y = 0f;
            tempRot.z = 0f;
            newBarrelRotation = Quaternion.Euler(tempRot);
        }

        #endregion

    }
}
