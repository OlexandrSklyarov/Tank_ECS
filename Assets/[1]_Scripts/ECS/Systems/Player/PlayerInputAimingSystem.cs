using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;

namespace SA.Tanks
{
    public struct PlayerInputAimingSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<AimingComponent, PlayerComponent> aimingFilter;
        readonly Camera mainCamera;

        #endregion


        public void Run()
        {
            var isLeftMouseDown = Input.GetMouseButtonDown(0);
            var isRightMousePressed = Input.GetMouseButton(1);
            var mousePosition = Input.mousePosition;

            foreach (var id in aimingFilter)
            {
                ref var aiming = ref aimingFilter.Get1(id);
                var playerEntity = aimingFilter.GetEntity(id);

                aiming.AimPosition = Vector3.zero;

                //если нажата п.к.м
                if (isRightMousePressed)
                {
                    var ray = mainCamera.ScreenPointToRay(mousePosition);

                    //если луч что либо пересёк, устанавливаем точку прицеливания
                    if (Physics.Raycast(ray, out RaycastHit hit, StaticPrm.Input.MAX_SHOOT_DISTANCE))
                    {
                        aiming.AimPosition = hit.point;
                    }

                    if (isLeftMouseDown)
                    {
                        playerEntity.Get<ShootingEvent>();
                    }
                }
            }
        }
    }
}
