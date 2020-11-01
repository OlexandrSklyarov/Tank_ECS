using LeoEcs.Pooling;

namespace SA.Tanks.Services
{
    public class GamePoolObject
    {     
        public PoolContainer Bullet { get; }
        public PoolContainer TankGreen { get; }
        public PoolContainer TankRed { get; }

        public GamePoolObject()
        {
            Bullet = PoolContainer.CreatePool<PoolObject>(StaticPrm.PoolPath.PATH_BULLET);
            TankGreen = PoolContainer.CreatePool<PoolObject>(StaticPrm.PoolPath.PATH_TANK_GREEN);
            TankRed = PoolContainer.CreatePool<PoolObject>(StaticPrm.PoolPath.PATH_TANK_RED);
        }
    }
}