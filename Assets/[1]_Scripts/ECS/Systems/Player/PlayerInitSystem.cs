﻿using Leopotam.Ecs;
using SA.Tanks.Data;
using UnityEngine;

namespace SA.Tanks
{
    public class PalayerInitSystem : IEcsInitSystem
    {
        #region Var

        EcsWorld _world;
        DataGame dataGame;

        #endregion


        #region Init

        public void Init()
        {
            var dataTank = dataGame.PlayerTank;

            //создаём сущьность
            var player = _world.NewEntity();

            //создаём объект и инициализируем компоненты
            var go = Object.Instantiate(dataTank.Prefab);

            //получаем компонент Rigidbody
            var rb = go.GetComponent<Rigidbody>();
            rb.maxAngularVelocity = dataTank.MaxAngularVelosity;

            //добавляем компоненты игроку
            AddPlayerComponent(dataTank, player, go);
            AddInputComponent(player);
            AddMoveComponent(dataTank, player, rb);
            AddTurretComponent(dataTank, player, go);
            AddWeaponComponent(player, dataGame.SimpleTankWeapon, go);
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


        void AddInputComponent(EcsEntity player)
        {
            player.Replace(new InputEventComponent());
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