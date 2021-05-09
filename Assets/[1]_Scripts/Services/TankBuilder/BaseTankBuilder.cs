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
        protected DataLevel level;
        protected DataTank dataTank;
        protected DataWeapon weapon;        
        protected Camera mainCamera;
        protected GamePool pool;

        protected EcsEntity entity;
        protected IPoolObject poolGO;

        #endregion       


        public abstract void SetUnitComponent();     
        
           
        public abstract void SetBraineComponent();


        public void Reset()
        {
            entity = world.NewEntity();
        }


        public void Create(Vector3 pos, Quaternion rot)
        {
            //создаём сущьность
            Reset();

            //создаём объект
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


        public virtual void SetMoveComponent()
        {
            //получаем компонент Rigidbody
            var rb = poolGO.PoolTransform.GetComponent<Rigidbody>();
            rb.maxAngularVelocity = dataTank.MaxAngularVelosity;
            rb.centerOfMass = dataTank.CentrOfMass;
            rb.mass = dataTank.Mass;

            //Debug.Log($"ground layer: {level.GroundLayer.ToString() }");

            entity.Replace(new MoveComponent()
            {
                RB = rb,
                GroundLayer = 1 << 6,
                Hits = new RaycastHit[6]   
            });

            entity.Replace(new TankEngineComponent()
            {
                Speed = dataTank.MoveSpeed,
                RotateSpeed = dataTank.RotateSpeed
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
                TurretTransform = provider.Turret,
                BarrelOriginTransform = provider.Barrel,
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

        
    }
}