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
                playerMoveFilter.Get1(id).Direction = new Vector2
                (
                    Input.GetAxis(StaticPrm.Input.HORIZONTAL), // x
                    Input.GetAxis(StaticPrm.Input.VERTICAL)  // y
                );
            }
        }
    }
}