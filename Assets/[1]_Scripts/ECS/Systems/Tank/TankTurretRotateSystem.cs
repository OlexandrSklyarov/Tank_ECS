using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct TankTurretRotateSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<TankTurretComponent, MoveComponent, AimingComponent> turretFilter;      

        #endregion


        #region Run

        public void Run()
        {
            foreach (var id in turretFilter)
            {
                ref var turret = ref turretFilter.Get1(id);
                var tankRB = turretFilter.Get2(id).RB;
                var aiming = turretFilter.Get3(id);

                //назначаем поворот по умолчанию, как у танка
                var newTurretRotation = tankRB.rotation;
                var newBarrelRotation = tankRB.rotation;
                var speed = turret.RotateSpeed * Time.deltaTime;

                //получаем поворот к цели, если она есть и она не слишком близко
                if (Vector3.Distance(aiming.AimPosition, turret.TurretTransform.position) > turret.MaxDistanceToTarget)
                {
                    //поворачиваем башню вокруг вертикальной оси танка (локально)                     
                    TurretRotation(ref turret, tankRB, aiming.AimPosition, speed);

                    //поворачиваем дуло танка
                    BarrelRorartion(ref turret, aiming.AimPosition, speed);
                }
                else
                {
                    DefaultRotation(ref turret, newBarrelRotation, newBarrelRotation, speed);
                }
            }
        }


        void DefaultRotation(ref TankTurretComponent turret, Quaternion newTurretRotation, Quaternion newBarrelRotation, float speed)
        {
            turret.TurretTransform.rotation = Quaternion.Lerp(turret.TurretTransform.rotation, newTurretRotation, speed);
            //turret.BarrelOriginTransform.rotation = Quaternion.Lerp(turret.BarrelOriginTransform.rotation, newBarrelRotation, speed * 0.5f);
        }


        void TurretRotation(ref TankTurretComponent turretComp, Rigidbody rb, Vector3 aimPosition, float speed)
        {
            var direction = aimPosition - turretComp.TurretTransform.position;    
            var tempRot = Quaternion.LookRotation(direction.normalized);
            turretComp.TurretTransform.rotation = Quaternion.Lerp(turretComp.TurretTransform.rotation, tempRot, speed);
            
            var locRot = turretComp.TurretTransform.localRotation;
            locRot.x = locRot.z = 0f;
            turretComp.TurretTransform.localRotation = locRot; 
        }


        private void BarrelRorartion(ref TankTurretComponent turretComp, Vector3 aimPosition, float speed)
        {
            // var direction = aimPosition - turretComp.TurretTransform.position;  

            // var angle = Vector3.Angle(turretComp.BarrelOriginTransform.forward, direction); 
            // angle = Mathf.Clamp(angle, -MAX_BARREL_ROTATE, turretComp.MAX_BARREL_ROTATE);              

            // var curRot = turretComp.BarrelOriginTransform.rotation;
            // var newRot = Quaternion.LookRotation(direction, Vector3.up);  

            // turretComp.BarrelOriginTransform.rotation = Quaternion.Lerp(curRot, newRot, speed);              
        }

        #endregion

    }
}
