
using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct EnemyCountObserverSystem : IEcsRunSystem
    {
        readonly EcsFilter<EnemyNumComponent, EnemySpawnPointComponent> spawnEnemyFilter;

        public void Run()
        {           
            foreach (var id in spawnEnemyFilter)
            {
                var spawnController = spawnEnemyFilter.GetEntity(id);

                ref var enemyNumber = ref spawnEnemyFilter.Get1(id);
                ref var spawnPoint = ref spawnEnemyFilter.Get2(id);

                if (enemyNumber.RemnantEnemies < 0) return;

                //если науровне меньше положеного количества врагов, создаем событие создания
                if (enemyNumber.EnemyExistCount < enemyNumber.MaxEnemyOnLevel)
                {
                    spawnController.Replace(new AddEnemyEvent());

                    enemyNumber.EnemyExistCount++;
                    enemyNumber.RemnantEnemies--;
                    spawnPoint.FreeSpawnPointIndex++;
                    Debug.Log($"Enemys: {enemyNumber.EnemyExistCount}");
                }
            }
        }
       
    }
}
