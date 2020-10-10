
using System;
using Leopotam.Ecs;
using UnityEngine;
using System.Linq;
using SA.Tanks.Extensions.UnityComponents;
using System.Collections.Generic;

namespace SA.Tanks
{
    public struct EnemyCountObserverSystem : IEcsRunSystem
    {
        readonly EcsFilter<EnemyNumComponent, EnemySpawnPointComponent> spawnEnemyFilter;

        const float CHECK_RADIUS = 5f;


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
                    //если удаётся назначить точку для спавна врага
                    if (SetNextFreeSpawnPoint(ref spawnPoint))
                    {
                        //событие создания нового врага
                        spawnController.Replace(new CreateNewEnemyEvent());
                    }
                }
            }
        }


        bool SetNextFreeSpawnPoint(ref EnemySpawnPointComponent esp)
        {
            var freePoints = new List<Transform>();

            //получаем список свободных точек
            foreach(var p in esp.AllSpawnPoints)            
                if (IsPointFree(p))
                    freePoints.Add(p);

            //устанавливаем рандомную точку, если таково нашлись
            esp.FreeSpawnPoint = (freePoints.Count > 1) ?
                freePoints[UnityEngine.Random.Range(0, freePoints.Count)] : 
                    (freePoints.Count != 0) ? freePoints.First() : null;

            return esp.FreeSpawnPoint != null;
        }


        //свободна ли точка от танков
        bool IsPointFree(Transform point)
        {
            var colliders = Physics.OverlapSphere(point.position, CHECK_RADIUS);

            //если найден танк, точка занята
            foreach (var c in colliders)
                if (c.GetComponent<EcsUnityProvider>())
                    return false;

            return true;
        }
    }
}
