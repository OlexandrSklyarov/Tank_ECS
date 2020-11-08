using System;
using LeoEcs.Pooling;
using SA.Tanks.Data;
using SA.Tanks.Services;

namespace SA.Tanks.Extensions.PoolGameObject
{
    public static class PoolExtension
    {
        public static PoolContainer GetTankPool(this GamePool pool, TankType tankType)
        {
            PoolContainer container;

            switch (tankType)
            {
                case TankType.TANK_GREEN:
                    container = pool.TankGreen;
                    break;
                case TankType.TANK_RED:
                    container = pool.TankRed;
                    break;
                    case TankType.TANK_PURPLE:
                    container = pool.TankPurple;
                    break;
                default:
                    throw new Exception($"{typeof(PoolExtension)} => TankType: {tankType} No pool was found with this type");
            }

            return container;
        }       
    }
}
