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
                if (Vector3.Distance(aiming.AimPosition, turret.TurretTransform.position) > MAX_DIST_TO_TARGET)
                {
                    //поворачиваем башню вокруг вертикальной оси танка (локально)                     
                    TurretRotation(aiming.AimPosition, turret, speed);                    

                    //поворачиваем дуло танка
                    BarrelRorartion(aiming.AimPosition, turret, speed);                                                   
                }  
                else
                {
                    DefaultRotation(turret, newBarrelRotation, newBarrelRotation, speed);
                }                     
            }
        }


        void DefaultRotation(TankTurretComponent turret, Quaternion newTurretRotation, Quaternion newBarrelRotation, float speed)
        {
            turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, newTurretRotation, speed);
            turret.BarrelOriginTransform.rotation = Quaternion.Lerp(turret.BarrelOriginTransform.rotation, newBarrelRotation, speed * 0.5f);
        }

        
        void TurretRotation(Vector3 aimPosition, TankTurretComponent turret, float speed)
        {            
            var direction = aimPosition - turret.TurretTransform.position;   

            var tempRot = Quaternion.LookRotation(direction);
            var tempDirection = tempRot.eulerAngles;    
            var newRot = Quaternion.Euler(0f, tempDirection.y, tempDirection.z);

            turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, newRot, speed);
        }


        private void BarrelRorartion(Vector3 aimPosition, TankTurretComponent turret, float speed)
        {
            var direction = aimPosition - turret.TurretTransform.position;  
            var angle = Vector3.Angle(turret.BarrelOriginTransform.forward, direction); 

            angle = Mathf.Clamp(angle, -MAX_BARREL_ROTATE, MAX_BARREL_ROTATE);                 
            var tempRot = turret.BarrelOriginTransform.rotation;
            var myRot = Quaternion.LookRotation(direction, Vector3.up);  

            turret.BarrelOriginTransform.rotation = Quaternion.Lerp(tempRot, myRot, speed);             
        }

        #endregion

    }
}
