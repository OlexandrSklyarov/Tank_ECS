using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct TankTurretRotateSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<TankTurretComponent, MoveComponent, AimingComponent> turretFilter;

        const float MAX_DIST_TO_TARGET = 4f;
        const float MAX_BARREL_ROTATE = 35f;
        

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
                var newBarrelRotation = tankRB.rotation;                
                var speed = turret.RotateSpeed * Time.deltaTime; 

                //получаем поворот к цели, если она есть и она не слишком близко
                if (aiming.IsTargetExist &&
                    Vector3.Distance(turret.Target, turret.TurretTransform.position) > MAX_DIST_TO_TARGET)
                {
                    //поворачиваем башню вокруг вертикальной оси танка (локально) 
                    
                    TurretRotation(aiming.AimPosition, turret, ref newTurretRotation);  
                    turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, newTurretRotation, speed);

                    BarrelRorartion(aiming.AimPosition, turret, ref newBarrelRotation);  
                    turret.BarrelTransform.rotation = Quaternion.Lerp(turret.BarrelTransform.rotation, newBarrelRotation, speed);                             
                }  
                else
                {
                    turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, newTurretRotation, speed);
                    turret.BarrelTransform.rotation = Quaternion.Lerp(turret.BarrelTransform.rotation, newBarrelRotation, speed);
                }                     
            }
        }

        
        void TurretRotation(Vector3 aimPosition, TankTurretComponent turret, ref Quaternion newTurretRotation)
        {            
            var direction = aimPosition - turret.TurretTransform.position;   
            newTurretRotation = Quaternion.LookRotation(direction);
            var tempRot = newTurretRotation.eulerAngles;
            tempRot.x = 0f;
            newTurretRotation = Quaternion.Euler(tempRot);
        }


        private void BarrelRorartion(Vector3 aimPosition, TankTurretComponent turret, ref Quaternion newBarrelRotation)
        {
            var direction = aimPosition - turret.BarrelTransform.position;  
            var angle = Vector3.Angle(turret.TurretTransform.forward, direction);

            newBarrelRotation = turret.TurretTransform.rotation;

            if (angle < MAX_BARREL_ROTATE)
            {
               newBarrelRotation = Quaternion.LookRotation(direction);               
            } 
        }

        #endregion

    }
}
