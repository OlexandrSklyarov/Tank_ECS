using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public class CheckGroundSystem : IEcsRunSystem
    {
        readonly EcsFilter<MoveComponent, VehicleComponent> _filter;


        public void Run()
        {
            foreach (var id in _filter)
            {
                ref var move = ref _filter.Get1(id);
                CheckGround(ref move);
            }
        }

        
        void CheckGround(ref MoveComponent move)
        {
            move.IsGrounded = false;
            
            foreach (var wheel in move.Wheels)
            {
                move.IsGrounded = wheel.isGrounded;
            }
        }
    }
}
