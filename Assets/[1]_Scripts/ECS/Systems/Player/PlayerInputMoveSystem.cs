using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;

namespace SA.Tanks
{
    public struct PlayerInputMoveSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<MoveComponent, PlayerComponent> playerMoveFilter;
        readonly Camera mainCamera;       

        #endregion


        public void Run()
        {
            foreach (var id in playerMoveFilter)
            {
                ref var moveComponent = ref playerMoveFilter.Get1(id);

                var x = Input.GetAxis(StaticPrm.Input.HORIZONTAL);
                var y = Input.GetAxis(StaticPrm.Input.VERTICAL);

                moveComponent.Direction = new Vector2(x, y);                  
            }
        }
    }
}