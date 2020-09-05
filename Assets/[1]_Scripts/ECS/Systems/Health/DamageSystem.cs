using System;
using Leopotam.Ecs;
using UnityEngine;

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
                    Debug.Log($"HP before: {healthComponent.HP}");
                    healthComponent.HP -= damage;  
                    Debug.Log($"HP^ {healthComponent.HP}");             
                }
                else 
                {
                    damageEntity.Replace(new DestroyComponentEvent());
                }    

                //добавляем компонент-событие изменения HP
                damageEntity.Replace(new ChangeHPBarEvent());            
            }
        }
    }
}
