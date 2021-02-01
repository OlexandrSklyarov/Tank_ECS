using Leopotam.Ecs;

namespace SA.Tanks
{
    public struct CameraFollowTargetSystem : IEcsRunSystem
    {
        #region Var

        EcsFilter<CameraFollowComponent> cameraFilter;
        EcsFilter<PlayerComponent> playerFilter;

        #endregion 


        #region Run

        public void Run()
        {
            //получаем компонент камеры
            foreach (var id in cameraFilter)
            {
                ref var camera = ref cameraFilter.Get1(id);

                //если у камеры нет цели
                if (!camera.IsTargetExist)
                {
                    //получаем компонент игрока
                    foreach (var playerid in playerFilter)
                    {                    
                        var player = playerFilter.Get1(playerid);

                        camera.VirtualCamera.Follow = player.Pivot;
                        camera.VirtualCamera.LookAt = player.RootTransform;
                        camera.IsTargetExist = true;
                    }
                }
            }
        }

        #endregion
    }
}