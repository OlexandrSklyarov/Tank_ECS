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
                //Enemy
                .Add(new EnemySpawnSystem())

                //Player
                .Add(new PalayerInitSystem())
                .Add(new PlayerInputAimingSystem())
                .Add(new PlayerInputMoveSystem())

                //Tank
                .Add(new AimingSystem())
                .Add(new ShootingSystem())
                .Add(new TankTurretRotateSystem())
                .Add(new MoveSystem())

                //Camera
                .Add(new InitCameraFollowSystem())
                .Add(new CameraFollowTargetSystem())

                //UI
                .Add(new ChangeTankUISystem())

                //Damage
                .Add(new TakeDamageSystem())
                .Add(new DestroyEntitySystem());

            //fixUpdateSystems
        }


        void RegistrationEvents()
        {
            updateSystems
                .OneFrame<DestroyComponentEvent>()
                .OneFrame<DamageComponentEvent>()
                .OneFrame<ShootingEvent>()
                .OneFrame<ChangeHPEvent>();
        }


        void Injected()
        {
            updateSystems.Inject(dataGame)
                .Inject(mainCamera)
                .Inject(enemySpawnPoints)
                .Inject(new GamePoolObject())
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