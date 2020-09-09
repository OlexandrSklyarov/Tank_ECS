using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;
using SA.Tanks.Extensions.PoolGameObject;
using SA.Tanks.Data;
using LeoEcs.Pooling;

namespace SA.Tanks
{
    public struct DestroyEntitySystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly GamePoolObject pool;

        readonly EcsFilter<DestroyComponentEvent, PoolObjectComponent, BulletComponent> bulletFilter;
        readonly EcsFilter<DestroyComponentEvent, PoolObjectComponent, EnemyComponent> enemyTankFilter;
        readonly EcsFilter<DestroyComponentEvent, PoolObjectComponent, PlayerComponent> playerTankFilter;
        readonly EcsFilter<EnemyNumComponent> enemySpawnController;

        #endregion


        public void Run()
        {
            DestroyBullet();
            DestroyEnemyTank();
            DestroyPlayerTank();
        }


        void DestroyBullet()
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


        void DestroyEnemyTank()
        {
            foreach (var numID in enemySpawnController)
            {
                //number controller
                ref var enemyCount = ref enemySpawnController.Get1(numID);

                foreach (var id in enemyTankFilter)
                {
                    var poolGO = enemyTankFilter.Get2(id).PoolGO;
                    var type = enemyTankFilter.Get3(id).TankType;
                    ReturnTankGo(type, poolGO);

                    var entity = enemyTankFilter.GetEntity(id);
                    entity.Destroy();

                    //меняем счетчик врагов на уровне
                    enemyCount.EnemyExistCount--;
                    Debug.Log($"Enemys: {enemyCount.EnemyExistCount}");
                }
            }
        }


        void DestroyPlayerTank()
        {
            foreach (var id in playerTankFilter)
            {
                var poolGO = playerTankFilter.Get2(id).PoolGO;
                var type = playerTankFilter.Get3(id).TankType;
                ReturnTankGo(type, poolGO);

                var entity = playerTankFilter.GetEntity(id);
                entity.Destroy();

                Debug.Log("Destroy Player Tank");
            }
        }


        //возвращает объект танка в пул по его типу
        void ReturnTankGo(TankType tankType, IPoolObject poolGO)
        {
            pool.GetTankPool(tankType).Recycle(poolGO);
        }

    }
}
