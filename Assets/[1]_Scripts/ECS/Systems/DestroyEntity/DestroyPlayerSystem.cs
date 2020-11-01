using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;
using SA.Tanks.Extensions.PoolGameObject;
using SA.Tanks.Data;
using LeoEcs.Pooling;

namespace SA.Tanks
{
    public struct DestroyPlayerSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly GamePool pool;

        readonly EcsFilter<DestroyComponentEvent, PoolObjectComponent, PlayerComponent> playerTankFilter;

        #endregion


        public void Run()
        {
            foreach (var id in playerTankFilter)
            {
                var poolGO = playerTankFilter.Get2(id).PoolGO;
                var tankType = playerTankFilter.Get3(id).TankType;
                ReturnTankToPool(tankType, poolGO);

                var entity = playerTankFilter.GetEntity(id);
                entity.Destroy();

                Debug.Log("Destroy Player Tank");
            }
        }


        //возвращает объект танка в пул по его типу
        void ReturnTankToPool(TankType tankType, IPoolObject poolGO)
        {
            pool.GetTankPool(tankType).Recycle(poolGO);
        }

    }
}
