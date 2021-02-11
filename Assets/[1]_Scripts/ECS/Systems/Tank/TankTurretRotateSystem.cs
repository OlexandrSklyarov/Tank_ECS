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
                    
                    TurretRotation(aiming.AimPosition, turret, ref newTurretRotation);  
                    turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, newTurretRotation, speed);

                    BarrelRorartion(aiming.AimPosition, turret, ref newBarrelRotation);  
                    //turret.BarrelOriginTransform.rotation = Quaternion.Lerp(turret.BarrelOriginTransform.rotation, newBarrelRotation, speed * 0.5f);                             
                }  
                else
                {
                    turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, newTurretRotation, speed);
                    //turret.BarrelOriginTransform.rotation = Quaternion.Lerp(turret.BarrelOriginTransform.rotation, newBarrelRotation, speed * 0.5f);
                }                     
            }
        }

        
        void TurretRotation(Vector3 aimPosition, TankTurretComponent turret, ref Quaternion newTurretRotation)
        {            
            var direction = aimPosition - turret.TurretTransform.position;   
            var tempRot = Quaternion.LookRotation(direction);
            var tempDirection = tempRot.eulerAngles;
            tempDirection.x = 0f;            
            newTurretRotation = Quaternion.Euler(tempDirection);
        }


        private void BarrelRorartion(Vector3 aimPosition, TankTurretComponent turret, ref Quaternion newBarrelRotation)
        {
            // var direction = aimPosition - turret.BarrelOriginTransform.position;  
            // var angle = Vector3.SignedAngle(direction, turret.TurretTransform.forward, turret.TurretTransform.right);                     

            // if (Mathf.Abs(angle) < MAX_BARREL_ROTATE)
            // {
            //     var projectDirection = Vector3.Project(direction, turret.BarrelOriginTransform.forward );
            //     projectDirection.y = direction.y;
            //     var tempRot = Quaternion.LookRotation(projectDirection);   
            //     tempRot.y = turret.BarrelOriginTransform.rotation.y;
            //     newBarrelRotation = tempRot;
            // } 
            // else
            // {
            //     newBarrelRotation = turret.TurretTransform.rotation;
            // }
        }

        #endregion

    }
}
