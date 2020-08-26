
using Leopotam.Ecs;
using UnityEngine;
using SA.Tanks.Data;

namespace SA.Tanks
{
    public struct EnemySpawnSystem : IEcsInitSystem
    {

        readonly EcsWorld _world;
        readonly Transform[] enemySpawnPoints;
        readonly DataGame dataGame;

        public void Init()
        {
            for (int i = 0; i < enemySpawnPoints.Length; i++)
            {
                var dataTank = GetRandomEnemy();

                //создаём сущьность
                var enemyEntity = _world.NewEntity();

                //создаём объект и инициализируем компоненты
                var point = enemySpawnPoints[i];
                var go = Object.Instantiate(dataTank.Prefab, point.position, Quaternion.identity);

                //получаем компонент Rigidbody
                var rb = go.GetComponent<Rigidbody>();
                rb.maxAngularVelocity = dataTank.MaxAngularVelosity;

                AddMoveComponent(dataTank, enemyEntity, rb);
            }
        }


        DataTank GetRandomEnemy()
        {
            var randomIndex = UnityEngine.Random.Range(0, dataGame.EnemyTank.Length);
            return dataGame.EnemyTank[randomIndex];
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
    }
}
