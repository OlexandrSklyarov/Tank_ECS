using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct ChangeHPBarSystem : IEcsRunSystem
    {
        #region Var

        EcsFilter<HealthComponent, TankUIComponent, ChangeHPBarEvent> changeHpBarFilter;

        #endregion


        public void Run()
        {
            foreach(var id in changeHpBarFilter)
            {
                ref var health = ref changeHpBarFilter.Get1(id);
                ref var ui = ref changeHpBarFilter.Get2(id);

                ChangeHPBar(ref health, ref ui);
            }
        }
    

        void ChangeHPBar(ref HealthComponent health, ref TankUIComponent ui)
        {
            var value = (1f / 1f) * health.HP;
            ui.HealthBar.fillAmount = value;
        }

    }
}