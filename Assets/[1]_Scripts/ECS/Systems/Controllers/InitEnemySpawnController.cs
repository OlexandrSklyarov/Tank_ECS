
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
        const int START_SPAWN_POINT_INDEX = 0;

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
                FreeSpawnPoint = enemySpawnPoints[START_SPAWN_POINT_INDEX],
                AllSpawnPoints = enemySpawnPoints
            });


        }
    }
}
