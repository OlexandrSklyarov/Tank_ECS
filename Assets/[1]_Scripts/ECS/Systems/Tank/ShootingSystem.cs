using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;
using SpaceInvadersLeoEcs.Pooling;

namespace SA.Tanks
{
    public struct ShootingSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsWorld _world;
        readonly PoolsGameObject pools;

        readonly EcsFilter<WaponComponent, ShootingEvent> shootingFilter;

        #endregion


        public void Run()
        {
            foreach(var id in shootingFilter)
            {
                ref var weapon = ref shootingFilter.Get1(id);
                ref var shootEvent = ref shootingFilter.Get2(id);

                SpawnBulet(weapon, shootEvent);
            }
        }


        void SpawnBulet(WaponComponent weapon, ShootingEvent shoot)
        {
            var shellEntity = _world.NewEntity();

            var poolGO = CreateShell(weapon.FirePoint);

            //add bullet component
            shellEntity.Replace(new BulletComponent()
            {
                Damage = weapon.Damage,
                ViewGO = poolGO
            });

            //push
            var rb = poolGO.PoolTransform.GetComponent<Rigidbody>();
            var force = rb.transform.forward * weapon.ShellSpeed;
            rb.AddForce(force, ForceMode.Impulse);
        }


        IPoolObject CreateShell(Transform spawnPoint)
        {
            var poolGO = pools.Bullets.Get();
            poolGO.PoolTransform.position = spawnPoint.position;
            poolGO.PoolTransform.rotation = spawnPoint.rotation;

            (poolGO as PoolObject).gameObject.SetActive(true);

            return poolGO;
        }
    }
}
