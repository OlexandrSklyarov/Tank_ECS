using SA.Tanks.Data;
using UnityEngine;
using Leopotam.Ecs;
using System;
using SA.Tanks.Services;

namespace SA.Tanks
{
    public class StarterGame : MonoBehaviour
    {
        #region Var

        [SerializeField] DataGame dataGame;

        EcsWorld world;
        EcsSystems updateSystems;
        EcsSystems fixUpdateSystems;

        Camera mainCamera;

        #endregion


        #region Init

        void Awake()
        {
            //проверяем наличие данных игры
            if(!dataGame) throw new Exception($"{nameof(DataGame)} doesn't exists!");

            world = new EcsWorld();
            updateSystems = new EcsSystems(world);
            fixUpdateSystems = new EcsSystems(world);

            mainCamera = Camera.main;

            AddSystems();
            RegistrationEvents();
            Injected();
            InitSystems();
        }


        void AddSystems()
        {
            updateSystems
                .Add(new PalayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new MoveSystem())
                .Add(new AimingSystem())
                .Add(new ShootingSystem())
                .Add(new InitCameraFollowSystem())
                .Add(new CameraFollowTargetSystem())
                .Add(new TankTurretRotateSystem());

            fixUpdateSystems.Add(new MoveSystem());
        }


        void RegistrationEvents()
        {
            updateSystems.OneFrame<ShootingEvent>();
        }


        void Injected()
        {
            updateSystems.Inject(dataGame)
                .Inject(mainCamera)
                .Inject(new PoolsGameObject())
                .ProcessInjects();
        }


        void InitSystems()
        {
            updateSystems.Init();
            fixUpdateSystems.Init();
        }

        #endregion


        #region Update

        void Update()
        {
            updateSystems.Run();
        }


        void FixedUpdate()
        {
            fixUpdateSystems.Run();
        }

        #endregion


        #region Destroy

        void OnDestroy()
        {
            updateSystems.Destroy();
            updateSystems = null;

            fixUpdateSystems.Destroy();
            fixUpdateSystems = null;

            world.Destroy();
            world = null;
        }

        #endregion

    }
}