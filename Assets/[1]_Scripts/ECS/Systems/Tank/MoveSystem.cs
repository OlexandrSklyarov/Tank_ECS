using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public class MoveSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<MoveComponent> moveFilter;

        #endregion


        public void Run()
        {
            PlayerMoving();
        }


        void PlayerMoving()
        {
            foreach (var i in moveFilter)
            {
                ref var moveComponent = ref moveFilter.Get1(i);

                Move(ref moveComponent);
                Rotate(ref moveComponent);
            }
        }


        void Move(ref MoveComponent move)
        {
            var forward = move.RB.transform.position
                    + move.RB.transform.forward
                    * move.Direction.y
                    * move.MoveSpeed * Time.deltaTime;

            move.RB.MovePosition(forward);
        }


        void Rotate(ref MoveComponent move)
        {
            var verticalRot = move.Direction.x * move.RotateSpeed;

            var rotation = move.RB.transform.rotation
                * Quaternion.Euler(0f, verticalRot, 0f);

            move.RB.MoveRotation(rotation);
        }
    }
}
