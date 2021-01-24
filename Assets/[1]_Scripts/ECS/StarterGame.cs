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
        [SerializeField] DataLevel dataLevel;
        [SerializeField] Transform[] enemySpawnPoints;
        [SerializeField] Transform[] waitpoints;

        EcsWorld world;
        EcsSystems updateSystems;
        EcsSystems fixedSystems;
       

        //EcsSystems fixUpdateSystems;

        Camera mainCamera;

        #endregion


        #region Init

        void Awake()
        {
            //проверяем наличие данных игры
            CheckGameData();

            world = new EcsWorld();
            updateSystems = new EcsSystems(world);
            fixedSystems = new EcsSystems(world);
           
            mainCamera = Camera.main;

            AddSystems();
            RegistrationEvents();
            Injected();
            InitSystems();
        }


        void CheckGameData()
        {
            if(!dataGame)
                throw new Exception($"{nameof(DataGame)} doesn't exists!");

            if(enemySpawnPoints == null || enemySpawnPoints.Length < 1) 
                throw new Exception($"EnemySpawnPoints doesn't exists!");
            
            if(waitpoints == null || waitpoints.Length < 1) 
                throw new Exception($"Waitpoints doesn't exists!");            
        }


        void AddSystems()
        {
            updateSystems
                //Player
                .Add(new PalayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerAimingSystem())                

                //Enemy
                .Add(new InitEnemySpawnController())
                .Add(new EnemyCountObserverSystem())
                .Add(new EnemySpawnSystem())
                .Add(new AIUpdateSystem())
                .Add(new EnemyAimingSystem())

                //Weapon
                .Add(new ShootingSystem())
                .Add(new WeaponReloadTimeSystem())                

                //Camera
                .Add(new InitCameraFollowSystem())
                .Add(new CameraFollowTargetSystem())               

                //Damage / Destroy
                .Add(new TakeDamageSystem())
                .Add(new DestroyPlayerSystem())
                .Add(new DestroyEnemySystem())
                .Add(new DestroyBulletSystem())

                //UI
                .Add(new ChangeTankHealthBarSystem());
            
            fixedSystems
                //Tank
                .Add(new TankTurretRotateSystem())
                .Add(new MoveSystem());
        }


        void RegistrationEvents()
        {
            updateSystems
                .OneFrame<CreateNewEnemyEvent>()
                .OneFrame<DestroyComponentEvent>()
                .OneFrame<DamageComponentEvent>()
                .OneFrame<ShootingEvent>()
                .OneFrame<ChangeHPEvent>();

            fixedSystems
                .OneFrame<InputMoveDirectionEvent>();
        }


        void Injected()
        {
            updateSystems
                .Inject(dataGame)
                .Inject(dataLevel)
                .Inject(mainCamera)
                .Inject(enemySpawnPoints)
                .Inject(waitpoints)
                .Inject(new GamePool())
                .Inject(new PlayerTankBuilder())
                .Inject(new EnemyTankBuilder())
                .ProcessInjects();
            
            fixedSystems
                .Inject(dataGame)
                .ProcessInjects();

        }


        void InitSystems()
        {
            updateSystems.Init();
            fixedSystems.Init();
        }

        #endregion


        #region Update   

        void Update()
        {
            updateSystems.Run();
        }

        void FixedUpdate()
        {            
            fixedSystems.Run();
        }

        #endregion


        #region Destroy

        void OnDestroy()
        {
            updateSystems.Destroy();
            updateSystems = null;

            fixedSystems.Destroy();
            fixedSystems = null;
                       
            world.Destroy();
            world = null;
        }

        #endregion

    }
}