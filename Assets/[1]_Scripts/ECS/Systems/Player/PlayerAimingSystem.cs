using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;

namespace SA.Tanks
{
    public struct PlayerAimingSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<AimingComponent, AimingEvent, PlayerComponent> aimingFilter;
        readonly Camera mainCamera;

        #endregion


        public void Run()
        {
            
            var mousePosition = Input.mousePosition;

            foreach (var id in aimingFilter)
            {
                ref var aiming = ref aimingFilter.Get1(id);
                var inputEvent = aimingFilter.Get2(id);
                var playerEntity = aimingFilter.GetEntity(id);               

                //если прицеливаемся
                if (inputEvent.IsAiming)
                {
                    var ray = mainCamera.ScreenPointToRay(mousePosition);

                    //если луч что либо пересёк, устанавливаем точку прицеливания
                    if (Physics.Raycast(ray, out RaycastHit hit, StaticPrm.Input.MAX_SHOOT_DISTANCE))
                    {
                        aiming.AimPosition = hit.point;
                        aiming.IsTargetExist = true;
                    }

                    //если нажимаем выстрел
                    if (inputEvent.IsFire)
                    {
                        playerEntity.Get<ShootingEvent>();
                    }
                }
                else
                {
                    aiming.IsTargetExist = false;
                }
            }
        }
    }
}
