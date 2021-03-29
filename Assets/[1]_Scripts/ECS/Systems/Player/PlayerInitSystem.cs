using Leopotam.Ecs;
using SA.Tanks.Data;
using SA.Tanks.Services;
using UnityEngine;

namespace SA.Tanks
{
    public class PalayerInitSystem : IEcsInitSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly DataGame dataGame;       
        readonly Camera mainCamera;
        readonly GamePool pool;
        readonly PlayerTankBuilder builder;
        readonly Transform playerSpawnPoint;

        #endregion


        #region Init

        public void Init()
        {
            builder.Setup(_world, dataGame, mainCamera, pool);

            builder.Create(playerSpawnPoint.position, Quaternion.identity);  
            builder.SetUnitComponent();
            builder.SetPoolObjectComponent();
            builder.SetHealthComponent();
            builder.SetUIComponent();
            builder.SetAimComponent();
            builder.SetMoveComponent();
            builder.SetTurretComponent();
            builder.SetWeaponComponent();  
        }

        #endregion     
    }
}