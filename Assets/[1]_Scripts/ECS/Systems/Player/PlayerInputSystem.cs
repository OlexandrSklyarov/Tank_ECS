using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;

namespace SA.Tanks
{
    public struct PlayerInputSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<PlayerComponent> playerFilter;
        readonly Camera mainCamera;       

        #endregion


        public void Run()
        {
            foreach (var id in playerFilter)
            {           
                ref var entity = ref playerFilter.GetEntity(id);

                InputMove(ref entity);  
                InputAim(ref entity);         
            }
        }


        void InputMove(ref EcsEntity entity)
        {
            var x = Input.GetAxis(StaticPrm.Input.HORIZONTAL);
            var y = Input.GetAxis(StaticPrm.Input.VERTICAL);    

            entity.Replace(new InputMoveDirectionEvent()
            {
                Horizontal = x,
                Vertical = y
            }); 
        }


        void InputAim(ref EcsEntity entity)
        {
            var isLeftMouseDown = Input.GetMouseButtonDown(0);
            var isRightMousePressed = Input.GetMouseButton(1);

            entity.Replace(new AimingEvent()
            {
                IsAiming = isRightMousePressed,
                IsFire = isLeftMouseDown
            });
        }
    }
}