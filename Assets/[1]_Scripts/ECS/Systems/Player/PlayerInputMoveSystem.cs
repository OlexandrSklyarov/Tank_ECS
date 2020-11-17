using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;

namespace SA.Tanks
{
    public struct PlayerInputMoveSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<MoveComponent, PlayerComponent> playerFilter;
        readonly Camera mainCamera;       

        #endregion


        public void Run()
        {
            foreach (var id in playerFilter)
            {                
                var x = Input.GetAxis(StaticPrm.Input.HORIZONTAL);
                var y = Input.GetAxis(StaticPrm.Input.VERTICAL);
                
                ref var entity = ref playerFilter.GetEntity(id);

                entity.Replace(new InputMoveDirectionEvent()
                {
                    Horizontal = x,
                    Vertical = y
                });             
            }
        }
    }
}