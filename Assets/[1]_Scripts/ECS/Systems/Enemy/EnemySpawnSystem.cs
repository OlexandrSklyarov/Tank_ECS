
using Leopotam.Ecs;
using UnityEngine;
using SA.Tanks.Data;
using UnityEngine.UI;

namespace SA.Tanks
{
    public struct EnemySpawnSystem : IEcsInitSystem
    {

        readonly EcsWorld _world;
        readonly Transform[] enemySpawnPoints;
        readonly DataGame dataGame;
        readonly Camera mainCamera;

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
                AddHealthComponent(enemyEntity, dataGame.PlayerTank.HP);
                AddTankUI(enemyEntity, go);
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


        private void AddTankUI(EcsEntity player, GameObject go)
        {
            //canvas
            var tankUI = go.transform.GetChild(1).GetComponent<Canvas>();
            tankUI.worldCamera = mainCamera;

            //hpBar => TankUI/HpBar/HpScale
            var hpBar = tankUI.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            hpBar.fillAmount = 1f; //max

            player.Replace(new TankUIComponent() 
            {                
                UITransform = tankUI.transform,                
                HealthBar = hpBar
            });
        }


        void AddHealthComponent(EcsEntity player, int hp)
        {
            player.Replace(new HealthComponent() { HP = hp});  
        }
    }
}
