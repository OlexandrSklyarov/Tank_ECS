using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public class MoveSystem : IEcsRunSystem
    {
        #region Var

        EcsFilter<MoveComponent, InputEventComponent> playerMoveFilter;

        #endregion


        public void Run()
        {
            PlayerMoving();
        }


        void PlayerMoving()
        {
            foreach (var i in playerMoveFilter)
            {
                ref var moveComponent = ref playerMoveFilter.Get1(i);
                ref var inputComponent = ref playerMoveFilter.Get2(i);

                Move(ref moveComponent, inputComponent.Direction);
                Rotate(ref moveComponent, inputComponent.Direction);
            }
        }


        void Move(ref MoveComponent move, Vector2 dir)
        {
            var forward = move.RB.transform.position
                    + move.RB.transform.forward
                    * dir.y
                    * move.MoveSpeed * Time.deltaTime;

            move.RB.MovePosition(forward);
        }


        void Rotate(ref MoveComponent move, Vector2 dir)
        {
            var verticalRot = dir.x * move.RotateSpeed;

            var rotation = move.RB.transform.rotation
                * Quaternion.Euler(0f, verticalRot, 0f);

            move.RB.MoveRotation(rotation);
        }
    }
}
