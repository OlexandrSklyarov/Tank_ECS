using System;
using Leopotam.Ecs;
namespace SA.Tanks
{
    public struct DamageSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<DamageComponentEvent, HealthComponent> damageFilter;

        #endregion


        public void Run()
        {
            foreach(var id in damageFilter)
            {
                var damageEntity = damageFilter.GetEntity(id);

                //если сущьность отключена, или не существует, выходим
                if (!damageEntity.IsAlive()) return;

                var damage = damageFilter.Get1(id).DamageAmount;

                ref var healthComponent = ref damageFilter.Get2(id);

                if (healthComponent.HP > 0)
                {
                    healthComponent.HP -= damage;
                }
                else 
                {
                    damageEntity.Replace(new DestroyComponentEvent());
                }
            }
        }
    }
}
