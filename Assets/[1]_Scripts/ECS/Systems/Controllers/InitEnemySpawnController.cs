
using Leopotam.Ecs;
using SA.Tanks.Data;
using UnityEngine;

namespace SA.Tanks
{
    public struct InitEnemySpawnController : IEcsPreInitSystem
    {
        readonly EcsWorld _world;
        readonly DataLevel dataLevel;
        readonly Transform[] enemySpawnPoints;

        const int ENEMY_COUNT = 0;
        const int SPAWN_POINT_INDEX = 0;

        public void PreInit()
        {
            var enemySpawnController = _world.NewEntity();

            enemySpawnController.Replace(new EnemyNumComponent()
            {
                RemnantEnemies = dataLevel.EnemyCount,
                MaxEnemyOnLevel = enemySpawnPoints.Length,
                EnemyExistCount = ENEMY_COUNT
            })
            .Replace(new EnemySpawnPointComponent()
            {
                FreeSpawnPointIndex = SPAWN_POINT_INDEX,
                MaxIndex = enemySpawnPoints.Length - 1
            });


        }
    }
}
