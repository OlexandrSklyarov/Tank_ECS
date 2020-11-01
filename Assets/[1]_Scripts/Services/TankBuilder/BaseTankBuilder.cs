using LeoEcs.Pooling;
using Leopotam.Ecs;
using SA.Tanks.Data;
using SA.Tanks.Extensions.PoolGameObject;
using SA.Tanks.Extensions.UnityComponents;
using UnityEngine;

namespace SA.Tanks.Services
{
    public abstract class BaseTankBuilder : ITankBuilder
    {
        #region Var

        protected EcsWorld world;
        protected DataTank dataTank;
        protected DataWeapon weapon;        
        protected Camera mainCamera;
        protected GamePool pool;

        protected EcsEntity entity;
        protected IPoolObject poolGO;

        #endregion       


        public abstract void SetUnitComponent();


        public virtual void Setup(EcsWorld world, DataGame dataGame, Camera mainCamera, GamePool pool)
        {
            this.world = world;
            dataTank = dataGame.PlayerTank;
            weapon = dataGame.SimpleTankWeapon;
            this.mainCamera = mainCamera;
            this.pool = pool;               
        }


        public void Reset()
        {
            entity = world.NewEntity();
        }


        public void Create(Vector3 pos, Quaternion rot)
        {
            //создаём сущьность
            Reset();

            //создаём объект и инициализируем компоненты
            poolGO = pool.GetTankPool(dataTank.TankType).Get();
            poolGO.PoolTransform.position = pos;
            poolGO.PoolTransform.rotation = rot;

            //активируем объект
            (poolGO as PoolObject).gameObject.SetActive(true);            

            //привязываем сущьность к объекту, 
            //для воозможности работать с сущьностью в физическеом мире
            poolGO.PoolTransform.GetProvider().SetEntity(entity);
        }


        public void SetAimComponent()
        {
            entity.Replace(new AimingComponent() { AimPosition = Vector3.zero});
        }


        public void SetHealthComponent()
        {
            entity.Replace(new HealthComponent() 
            { 
                HP = dataTank.HP,
                MaxHP = dataTank.MaxHP
            });
        }


        public void SetMoveComponent()
        {
            //получаем компонент Rigidbody
            var rb = poolGO.PoolTransform.GetComponent<Rigidbody>();
            rb.maxAngularVelocity = dataTank.MaxAngularVelosity;

            entity.Replace(new MoveComponent()
            {
                MoveSpeed = dataTank.MoveSpeed,
                RotateSpeed = dataTank.RotateSpeed,
                RB = rb
            });
        }


        public void SetPoolObjectComponent()
        {
            entity.Replace(new PoolObjectComponent()
            { 
                PoolGO = poolGO
            });
        }


        public void SetTurretComponent()
        {
            var provider = poolGO.PoolTransform.GetComponent<TankProvider>();

            entity.Replace(new TankTurretComponent()
            {
                TurretTransform = provider.TankTurret,
                RotateSpeed = dataTank.TurretSpeedRotate
            });
        }


        public void SetUIComponent()
        {
            var provider = poolGO.PoolTransform.GetComponent<TankProvider>();

            //canvas
            var tankUI = provider.TankCanvas;
            tankUI.worldCamera = mainCamera;

            entity.Replace(new TankUIComponent() 
            {                
                UITransform = tankUI.transform,                
                HealthBar = provider.HPBar
            });

            entity.Replace(new ChangeHPEvent());
        }        


        public void SetWeaponComponent()
        {
            var provider = poolGO.PoolTransform.GetComponent<TankProvider>();

            entity.Replace(new WaponComponent()
            {
                FirePoint = provider.FirePoint,
                ShellSpeed = weapon.Speed,
                Damage = weapon.Damage,
                ReloadTime = weapon.ReloadTime
            });
        }

        public void SetBraineComponent()
        {
            Debug.Log("Brain => player controls !!!");
        }
    }
}