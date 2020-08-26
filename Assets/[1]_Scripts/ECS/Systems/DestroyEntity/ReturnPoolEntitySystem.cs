using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;

namespace SA.Tanks
{
    public struct ReturnPoolEntitySystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly PoolsGameObject pools;

        readonly EcsFilter<DestroyComponentEvent, PoolObjectComponent, BulletComponent> bulletFilter;

        #endregion


        public void Run()
        {
            ReturnBulletToPool();
        }


        void ReturnBulletToPool()
        {
            foreach (var id in bulletFilter)
            {
                var poolGO = bulletFilter.Get2(id).PoolGO;
                pools.Bullets.Recycle(poolGO);

                var entity = bulletFilter.GetEntity(id);
                entity.Destroy();

                Debug.Log("Destroy bullet");
            }
        }

    }
}
