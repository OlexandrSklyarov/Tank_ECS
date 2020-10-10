using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;

namespace SA.Tanks
{
    public struct DestroyBulletSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly GamePoolObject pool;

        readonly EcsFilter<DestroyComponentEvent, PoolObjectComponent, BulletComponent> bulletFilter;

        #endregion


        public void Run()
        {
            foreach (var id in bulletFilter)
            {
                var poolGO = bulletFilter.Get2(id).PoolGO;
                poolGO.PoolTransform.GetComponent<Rigidbody>().isKinematic = true;

                pool.Bullet.Recycle(poolGO);

                var entity = bulletFilter.GetEntity(id);
                entity.Destroy();
            }
        }
    }
}
