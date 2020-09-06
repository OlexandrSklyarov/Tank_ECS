using System;
using LeoEcs.Pooling;
using Leopotam.Ecs;
using SA.Tanks.Data;
using SA.Tanks.Extensions.PoolGameObject;
using SA.Tanks.Extensions.UnityComponents;
using SA.Tanks.Services;
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
        readonly GamePoolObject pool;

        #endregion


        #region Init

        public void Init()
        {
            var dataTank = dataGame.PlayerTank;
            var weapon = dataGame.SimpleTankWeapon;

            //создаём сущьность
            var entity = _world.NewEntity();

            //создаём объект и инициализируем компоненты
            var poolGO = CreatePlayerTank(dataTank.TankType, Vector3.zero, Quaternion.identity);

            //привязываем сущьность к объекту, 
            //для воозможности работать с сущьностью в физическеом мире
            poolGO.PoolTransform.GetProvider().SetEntity(entity);

            //получаем компонент Rigidbody
            var rb = poolGO.PoolTransform.GetComponent<Rigidbody>();
            rb.maxAngularVelocity = dataTank.MaxAngularVelosity;

            var tr = poolGO.PoolTransform;

            //добавляем компоненты игроку
            AddPlayerComponent(dataTank, entity, tr);
            AddPoolObjectComponent(entity, poolGO);
            AddHealthComponent(entity, dataTank.HP, dataTank.MaxHP);
            AddTankUI(entity, tr);
            AddAimingComponent(entity);
            AddMoveComponent(dataTank, entity, rb);
            AddTurretComponent(dataTank, entity, tr);
            AddWeaponComponent(entity, weapon, tr);

        }


        IPoolObject CreatePlayerTank(TankType tankType, Vector3 pos, Quaternion rot)
        {
            var poolGO = pool.GetTankPool(tankType).Get();
            poolGO.PoolTransform.position = pos;
            poolGO.PoolTransform.rotation = rot;

            (poolGO as PoolObject).gameObject.SetActive(true);

            return poolGO;
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


        void AddTankUI(EcsEntity entity, Transform tr)
        {
            //canvas
            var tankUI = tr.GetChild(1).GetComponent<Canvas>();
            tankUI.worldCamera = mainCamera;

            //hpBar => TankUI/HpBar/HpScale
            var hpBar = tankUI.transform.GetChild(0).GetChild(0).GetComponent<Image>();

            entity.Replace(new TankUIComponent() 
            {                
                UITransform = tankUI.transform,                
                HealthBar = hpBar
            });

            entity.Replace(new ChangeHPEvent());
        }


        void AddHealthComponent(EcsEntity player, int hp, int maxHp)
        {
            player.Replace(new HealthComponent() 
            { 
                HP = hp,
                MaxHP = maxHp
            });  
        }


        void AddWeaponComponent(EcsEntity player, DataWeapon weapon, Transform tr)
        {
            player.Replace(new WaponComponent()
            {
                FirePoint = tr //tank
                    .GetChild(0).transform //model
                    .GetChild(1).transform // turret
                    .GetChild(1).transform, //fire point

                ShellSpeed = weapon.Speed,
                Damage = weapon.Damage
            });
        }


        void AddTurretComponent(DataTank data, EcsEntity player, Transform tr)
        {
            var turret = tr //tank
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


        void AddPlayerComponent(DataTank data, EcsEntity player, Transform tr)
        {
            //добавляем компонент игрока
            var pivot = new GameObject("Pivot");
            pivot.transform.SetParent(tr);
            pivot.transform.localPosition = data.CameraPivotPosition;
            player.Replace(new PlayerComponent()
            {
                TankType = data.TankType,
                RootTransform = tr,
                Pivot = pivot.transform
            });
        }

        #endregion

    }
}