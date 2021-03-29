using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;
using LeoEcs.Pooling;
using SA.Tanks.Extensions.UnityComponents;

namespace SA.Tanks
{
    public struct ShootingSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly GamePool pool;
        readonly EcsFilter<WaponComponent, ShootingEvent> shooterFilter;

        #endregion


        public void Run()
        {
            foreach(var id in shooterFilter)
            {
                ref var weapon = ref shooterFilter.Get1(id);

                if (weapon.LastFireTime > 0f)
                {
                    weapon.LastFireTime -= Time.deltaTime * 1f;
                }
                else
                {
                    SpawnBulet(ref weapon);
                    weapon.LastFireTime = weapon.ReloadTime;
                }                
            }
        }


        void SpawnBulet(ref WaponComponent weapon)
        {
            var shellEntity = _world.NewEntity();

            var poolGO = CreateShell(weapon.FirePoint);

            //привязываем сущьность к объекту, 
            //для воозможности работать с сущьностью в физическеом мире
            poolGO.PoolTransform.GetProvider().SetEntity(shellEntity);

            //add bullet component
            shellEntity.Replace(new BulletComponent() 
            { 
                Damage = weapon.Damage,
                LifeTime = 0f,
                MaxLifeTime = 5f
            });


            shellEntity.Replace(new PoolObjectComponent() { PoolGO = poolGO });

            //push
            Fire(ref weapon, ref poolGO);           
        }


        void Fire(ref WaponComponent weapon, ref IPoolObject poolGO)
        {
            var rb = poolGO.PoolTransform.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            var force = rb.transform.forward * weapon.ShellSpeed;
            rb.AddForce(force, ForceMode.Impulse);           
        }


        IPoolObject CreateShell(Transform spawnPoint)
        {
            var poolGO = pool.Bullet.Get();
            poolGO.PoolTransform.position = spawnPoint.position;
            poolGO.PoolTransform.rotation = spawnPoint.rotation;

            (poolGO as PoolObject).gameObject.SetActive(true);

            return poolGO;
        }
    }
}
