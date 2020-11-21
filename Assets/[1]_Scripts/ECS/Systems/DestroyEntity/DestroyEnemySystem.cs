using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;
using SA.Tanks.Extensions.PoolGameObject;
using SA.Tanks.Data;
using LeoEcs.Pooling;

namespace SA.Tanks
{
    public struct DestroyEnemySystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly GamePool pool;

        readonly EcsFilter<DestroyComponentEvent, PoolObjectComponent, EnemyComponent> enemyTankFilter;
        readonly EcsFilter<EnemyNumComponent> enemySpawnController;

        #endregion


        public void Run()
        {
            foreach (var numID in enemySpawnController)
            {
                //number controller
                ref var enemyCount = ref enemySpawnController.Get1(numID);

                foreach (var id in enemyTankFilter)
                {
                    var poolGO = enemyTankFilter.Get2(id).PoolGO;
                    var tankType = enemyTankFilter.Get3(id).TankType;
                    ReturnTankToPool(tankType, poolGO);

                    var entity = enemyTankFilter.GetEntity(id);
                    entity.Destroy();

                    //меняем счетчик врагов на уровне
                    enemyCount.EnemyExistCount--;
                }
            }
        }


        //возвращает объект танка в пул по его типу
        void ReturnTankToPool(TankType tankType, IPoolObject poolGO)
        {
            pool.GetTankPool(tankType).Recycle(poolGO);
        }

    }
}
