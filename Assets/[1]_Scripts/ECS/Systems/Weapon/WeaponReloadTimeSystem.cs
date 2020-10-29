using Leopotam.Ecs;
using SA.Tanks.Services;
using UnityEngine;
using LeoEcs.Pooling;
using SA.Tanks.Extensions.UnityComponents;

namespace SA.Tanks
{
    public struct WeaponReloadTimeSystem : IEcsRunSystem
    {
        #region Var
        
        readonly EcsFilter<WaponComponent> reloadTimeFilter;

        #endregion


        public void Run()
        {
            foreach(var id in reloadTimeFilter)
            {
                ref var weapon = ref reloadTimeFilter.Get1(id);

                if (weapon.LastFireTime > 0f)
                {
                    weapon.LastFireTime -= Time.deltaTime * 1f;
                    Debug.Log($"Reload time: {weapon.LastFireTime}");
                }                                
            }
        }
    }
}