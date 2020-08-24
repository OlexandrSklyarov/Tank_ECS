using Leopotam.Ecs;
using Cinemachine;

namespace SA.Tanks
{
    public struct InitCameraFollowSystem : IEcsInitSystem
    {
        #region Var

        readonly EcsWorld _world;

        #endregion


        #region Init

        public void Init()
        {
            var vc = UnityEngine.Object.FindObjectOfType<CinemachineVirtualCamera>();

            var cameraEntity = _world.NewEntity();

            cameraEntity.Replace(new CameraFollowComponent()
            {
                VirtualCamera = vc
            });

        }

        #endregion
    }
}
