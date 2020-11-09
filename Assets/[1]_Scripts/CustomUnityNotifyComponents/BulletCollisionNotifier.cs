using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks.Extensions.UnityComponents
{
    public class BulletCollisionNotifier : EcsUnityNotifierBase
    {
        private void OnTriggerEnter(Collider other)
        {     
            if(!Entity.IsAlive()) return;         

            //добавляем снаряду компонент-событие об уничтожении
            Entity.Replace(new DestroyComponentEvent());
           
            var otherTransform = other.transform;
            if (!otherTransform.HasProvider()) return;

            //пытаемся получить сущьность су объекта с которым столкнулись
            var otherEntity = otherTransform.GetProvider().Entity;
            if (!otherEntity.IsAlive()) return;

            //добавляем компонент урона на сущьность с которой столкнулся снаряд
            var bulletDamage = Entity.Get<BulletComponent>().Damage;
            otherEntity.Replace(new DamageComponentEvent() {DamageAmount = bulletDamage });
        }
    }
}