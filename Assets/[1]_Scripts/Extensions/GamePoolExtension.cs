using System;
using LeoEcs.Pooling;
using SA.Tanks.Data;
using SA.Tanks.Services;

namespace SA.Tanks.Extensions.PoolGameObject
{
    public static class PoolExtension
    {
        public static PoolContainer GetTankPool(this GamePoolObject pool, TankType tankType)
        {
            PoolContainer container;

            switch (tankType)
            {
                case TankType.TANK_1:
                    container = pool.TankGreen;
                    break;
                case TankType.TANK_2:
                    container = pool.TankRed;
                    break;
                default:
                    throw new Exception($"{typeof(PoolExtension)} => TankType: {tankType} No pool was found with this type");
            }

            return container;
        }       
    }
}
