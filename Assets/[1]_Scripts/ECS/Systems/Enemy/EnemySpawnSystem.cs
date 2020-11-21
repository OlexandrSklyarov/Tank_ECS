using Leopotam.Ecs;
using UnityEngine;
using SA.Tanks.Data;
using SA.Tanks.Services;

namespace SA.Tanks
{
    public struct EnemySpawnSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly Transform[] enemySpawnPoints;
        readonly Transform[] waitpoints;
        readonly DataGame dataGame;
        readonly Camera mainCamera;
        readonly GamePool pool;
        readonly EnemyTankBuilder builder;

        readonly EcsFilter<EnemyNumComponent, EnemySpawnPointComponent, CreateNewEnemyEvent> enemySpawnController;

        #endregion


        #region Init

        public void Run()
        {
            //если нашлась сущьность с таким компонентом, создаем врага
            foreach(var id in enemySpawnController)
            {
                var point = enemySpawnController.Get2(id).FreeSpawnPoint;
                ref var num = ref enemySpawnController.Get1(id);

                if (point)
                {
                    CreateEnemy(point);

                    //изменяем счёт танков на поле
                    num.EnemyExistCount++;
                    num.RemnantEnemies--;
                }
            }
        }

       
        void CreateEnemy(Transform point)
        {           
            builder.Setup(_world, dataGame, mainCamera, pool, waitpoints);

            builder.Create(point.position, point.rotation);  
            builder.SetUnitComponent();
            builder.SetPoolObjectComponent();
            builder.SetHealthComponent();
            builder.SetUIComponent();
            builder.SetAimComponent();
            builder.SetMoveComponent();
            builder.SetTurretComponent();
            builder.SetWeaponComponent(); 
            builder.SetBraineComponent();
        }        

        #endregion
    }
}
