using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct TakeDamageSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<DamageComponentEvent, HealthComponent> damageFilter;

        #endregion


        public void Run()
        {
            foreach(var id in damageFilter)
            {
                var damageEntity = damageFilter.GetEntity(id);

                //если сущьность существует
                if (!damageEntity.IsAlive()) return;

                var damage = damageFilter.Get1(id).DamageAmount;

                ref var healthComponent = ref damageFilter.Get2(id);

                healthComponent.HP -= damage;

                if (healthComponent.HP <= 0)
                {                
                    damageEntity.Replace(new DestroyComponentEvent());
                }

                //добавляем компонент-событие изменения HP
                damageEntity.Replace(new ChangeHPEvent());
            }
        }
    }
}
