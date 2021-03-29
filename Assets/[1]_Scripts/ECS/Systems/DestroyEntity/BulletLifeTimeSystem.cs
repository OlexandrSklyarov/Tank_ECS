using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct BulletLifeTimeSystem : IEcsRunSystem
    {
        #region Var

        EcsFilter<BulletComponent> bulletFilter;

        #endregion


        public void Run()
        {

            foreach (var id in bulletFilter)
            {
                ref var comp = ref bulletFilter.Get1(id);                

                if (comp.LifeTime >= comp.MaxLifeTime)
                {                
                    bulletFilter.GetEntity(id).Replace(new DestroyComponentEvent());
                }
                else
                {
                    comp.LifeTime += Time.deltaTime;
                }
            }            
        }
    }
}
