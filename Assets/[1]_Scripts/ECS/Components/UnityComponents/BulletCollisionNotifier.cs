﻿using Leopotam.Ecs;
using SA.Tanks;
using SA.Tanks.Extensions.UnityComponents;
using UnityEngine;

namespace SpaceInvadersLeoEcs.UnityComponents
{
    public class BulletCollisionNotifier : EcsUnityNotifierBase
    {
        private void OnTriggerEnter(Collider other)
        {     
            if(!Entity.IsAlive()) return;         

            //добавляем снаряду компонент-событие об уничтожении
            Entity.Replace(new DestroyComponentEvent());

            Debug.Log(Entity.Id);
           
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