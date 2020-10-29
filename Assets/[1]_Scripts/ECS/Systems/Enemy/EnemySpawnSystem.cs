using SA.Tanks.Extensions.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using SA.Tanks.Data;
using UnityEngine.UI;
using LeoEcs.Pooling;
using SA.Tanks.Services;
using SA.Tanks.Extensions.PoolGameObject;

namespace SA.Tanks
{
    public struct EnemySpawnSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly Transform[] enemySpawnPoints;
        readonly DataGame dataGame;
        readonly Camera mainCamera;
        readonly GamePoolObject pool;

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
                    Debug.Log($"Enemys: {num.EnemyExistCount}");
                }
            }
        }

       
        void CreateEnemy(Transform point)
        {           
            var dataTank = GetRandomEnemy(dataGame);

            //создаём объект и инициализируем компоненты
            var poolGO = CreateTankGO(dataTank.TankType, point);

            //создаём сущьность
            var enemyEntity = _world.NewEntity();

            //привязываем сущьность к объекту, 
            //для воозможности работать с сущьностью в физическеом мире
            poolGO.PoolTransform.GetProvider().SetEntity(enemyEntity);

            //получаем компонент Rigidbody
            var rb = poolGO.PoolTransform.GetComponent<Rigidbody>();
            rb.maxAngularVelocity = dataTank.MaxAngularVelosity;

            var tr = poolGO.PoolTransform;

            var provider = tr.GetComponent<TankProvider>();

            AddEnemyComponent(enemyEntity, dataTank.TankType);
            AddPoolObjectComponent(enemyEntity, poolGO);
            AddMoveComponent(dataTank, enemyEntity, rb);
            AddHealthComponent(enemyEntity, dataTank.HP, dataTank.MaxHP);
            AddTankUI(enemyEntity, provider);
        }
               

        IPoolObject CreateTankGO(TankType tankType, Transform spawnPoint)
        {
            var poolGO = pool.GetTankPool(tankType).Get();
            poolGO.PoolTransform.position = spawnPoint.position;
            poolGO.PoolTransform.rotation = spawnPoint.rotation;

            (poolGO as PoolObject).gameObject.SetActive(true);

            return poolGO;
        }


        DataTank GetRandomEnemy(DataGame data)
        {
            var randomIndex = UnityEngine.Random.Range(0, data.EnemyTank.Length);
            return data.EnemyTank[randomIndex];
        }

        #endregion


        #region Component

        void AddPoolObjectComponent(EcsEntity entity, IPoolObject poolGO)
        {
            entity.Replace(new PoolObjectComponent()
            {
                PoolGO = poolGO
            });
        }


        void AddEnemyComponent(EcsEntity entity, TankType tankType)
        {
            entity.Replace(new EnemyComponent()
            { 
                TankType = tankType
            });
        }


        void AddMoveComponent(DataTank data, EcsEntity entity, Rigidbody rb)
        {
            //move
            entity.Replace(new MoveComponent()
            {
                MoveSpeed = data.MoveSpeed,
                RotateSpeed = data.RotateSpeed,
                RB = rb
            });
        }


         void AddTankUI(EcsEntity entity, TankProvider provider)
        {    
            //canvas
            var tankUI = provider.TankCanvas;
            tankUI.worldCamera = mainCamera;

            entity.Replace(new TankUIComponent() 
            {                
                UITransform = tankUI.transform,                
                HealthBar = provider.HPBar
            });

            entity.Replace(new ChangeHPEvent());
        }


        void AddHealthComponent(EcsEntity entity, int hp, int maxHp)
        {
            entity.Replace(new HealthComponent()
            {
                HP = hp,
                MaxHP = maxHp
            });
        }

        #endregion
    }
}
