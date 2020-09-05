using Leopotam.Ecs;
using SA.Tanks.Data;
using SA.Tanks.Extensions.UnityComponents;
using UnityEngine;
using UnityEngine.UI;

namespace SA.Tanks
{
    public class PalayerInitSystem : IEcsInitSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly DataGame dataGame;
        readonly Camera mainCamera;

        #endregion


        #region Init

        public void Init()
        {
            var dataTank = dataGame.PlayerTank;

            //создаём сущьность
            var player = _world.NewEntity();

            //создаём объект и инициализируем компоненты
            var go = Object.Instantiate(dataTank.Prefab);

            //привязываем сущьность к объекту, 
            //для воозможности работать с сущьностью в физическеом мире
            go.transform.GetProvider().SetEntity(player);

            //получаем компонент Rigidbody
            var rb = go.GetComponent<Rigidbody>();
            rb.maxAngularVelocity = dataTank.MaxAngularVelosity;

            //добавляем компоненты игроку
            AddPlayerComponent(dataTank, player, go);
            AddAimingComponent(player);
            AddMoveComponent(dataTank, player, rb);
            AddTurretComponent(dataTank, player, go);
            AddWeaponComponent(player, dataGame.SimpleTankWeapon, go);
            AddHealthComponent(player, dataGame.PlayerTank.HP);
            AddTankUI(player, go);
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


        static void AddWeaponComponent(EcsEntity player, DataWeapon weapon, GameObject go)
        {
            player.Replace(new WaponComponent()
            {
                FirePoint = go.transform //tank
                    .GetChild(0).transform //model
                    .GetChild(1).transform // turret
                    .GetChild(1).transform, //fire point

                ShellSpeed = weapon.Speed,
                Damage = weapon.Damage
            });
        }


        void AddTurretComponent(DataTank data, EcsEntity player, GameObject go)
        {
            var turret = go.transform //tank
                .GetChild(0).transform //model
                .GetChild(1).transform; // turret


            player.Replace(new TankTurretComponent()
            {
                TurretTransform = turret,
                RotateSpeed = data.TurretSpeedRotate
            });
        }


        void AddMoveComponent(DataTank data, EcsEntity player, Rigidbody rb)
        {
            //move
            player.Replace(new MoveComponent()
            {
                MoveSpeed = data.MoveSpeed,
                RotateSpeed = data.RotateSpeed,
                RB = rb
            });
        }


        void AddAimingComponent(EcsEntity player)
        {
            player.Replace(new AimingComponent() { AimPosition = Vector3.zero});
        }


        void AddPlayerComponent(DataTank data, EcsEntity player, GameObject go)
        {
            //добавляем компонент игрока
            var pivot = new GameObject("Pivot");
            pivot.transform.SetParent(go.transform);
            pivot.transform.localPosition = data.CameraPivotPosition;
            player.Replace(new PlayerComponent()
            {
                PlayerTransform = go.transform,
                Pivot = pivot.transform
            });
        }

        #endregion

    }
}