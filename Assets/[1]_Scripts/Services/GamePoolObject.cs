using LeoEcs.Pooling;

namespace SA.Tanks.Services
{
    public class GamePoolObject
    {     
        public PoolContainer Bullet { get; }
        public PoolContainer Tank_1 { get; }
        public PoolContainer Tank_2 { get; }

        public GamePoolObject()
        {
            Bullet = PoolContainer.CreatePool<PoolObject>(StaticPrm.PoolPath.PATH_BULLET);
            Tank_1 = PoolContainer.CreatePool<PoolObject>(StaticPrm.PoolPath.PATH_TANK_1);
            Tank_2 = PoolContainer.CreatePool<PoolObject>(StaticPrm.PoolPath.PATH_TANK_2);
        }
    }
}