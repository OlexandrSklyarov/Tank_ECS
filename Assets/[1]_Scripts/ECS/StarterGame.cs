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
        [SerializeField] GameObject[] walls;
        [SerializeField] Transform[] enemySpawnPoints;

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
                .Add(new WallInitSystem())

                .Add(new EnemySpawnSystem())

                .Add(new PalayerInitSystem())
                .Add(new PlayerInputAimingSystem())
                .Add(new PlayerInputMoveSystem())

                .Add(new AimingSystem())
                .Add(new ShootingSystem())
                .Add(new TankTurretRotateSystem())

                .Add(new InitCameraFollowSystem())
                .Add(new CameraFollowTargetSystem())

                .Add(new MoveSystem())
                .Add(new DamageSystem())

                .Add(new ReturnPoolEntitySystem());

            //fixUpdateSystems
        }


        void RegistrationEvents()
        {
            updateSystems
                .OneFrame<DestroyComponentEvent>()
                .OneFrame<DamageComponentEvent>()
                .OneFrame<ShootingEvent>();
        }


        void Injected()
        {
            updateSystems.Inject(dataGame)
                .Inject(mainCamera)
                .Inject(walls)
                .Inject(enemySpawnPoints)
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