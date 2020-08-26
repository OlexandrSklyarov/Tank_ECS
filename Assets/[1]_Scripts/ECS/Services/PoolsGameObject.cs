using LeoEcs.Pooling;

namespace SA.Tanks.Services
{
    public class PoolsGameObject
    {     
        public PoolContainer Bullets { get; }
        public PoolContainer EnemyTanks { get; }

        public PoolsGameObject()
        {
            Bullets = PoolContainer.CreatePool<PoolObject>(StaticPrm.PoolPath.PATH_BULLET);
            EnemyTanks = PoolContainer.CreatePool<PoolObject>(StaticPrm.PoolPath.PATH_ENEMY_TANK);
        }
    }
}