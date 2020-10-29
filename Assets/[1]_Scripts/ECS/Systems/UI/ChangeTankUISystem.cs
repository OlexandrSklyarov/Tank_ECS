using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct ChangeTankHealthBarSystem : IEcsRunSystem
    {
        #region Var

        EcsFilter<HealthComponent, TankUIComponent, ChangeHPEvent> changeHpBarFilter;

        #endregion


        public void Run()
        {
            ChangeHealthBar();
        }
    

        void ChangeHealthBar()
        {
            foreach (var id in changeHpBarFilter)
            {
                ref var health = ref changeHpBarFilter.Get1(id);
                ref var ui = ref changeHpBarFilter.Get2(id);

                var value = (1f / health.MaxHP) * health.HP;
                ui.HealthBar.fillAmount = Mathf.Clamp(value, 0f, 1f);
            }
        }
    }
}