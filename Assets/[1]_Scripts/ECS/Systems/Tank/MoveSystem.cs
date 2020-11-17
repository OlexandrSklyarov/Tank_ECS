using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public class MoveSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<MoveComponent, InputMoveDirectionEvent> moveFilter;       

        #endregion


        public void Run()
        {
            foreach (var id in moveFilter)
            {
                ref var moveComponent = ref moveFilter.Get1(id);
                ref var input = ref moveFilter.Get2(id);

                Move(ref moveComponent, ref input);
                Rotate(ref moveComponent, ref input);                
            }
        }


        void Move(ref MoveComponent move, ref InputMoveDirectionEvent input)
        {
            var forward = move.RB.transform.position
                        + move.RB.transform.forward
                        * input.Vertical
                        * move.MoveSpeed * Time.deltaTime;

            //Drag
            if (forward.magnitude > move.MinDrag)
            {
                move.RB.drag = move.MinDrag;
            }
            else if (move.RB.drag < move.MaxDrag)
            {
                move.RB.drag += move.DragValue;
            }          

            //Move
            move.RB.MovePosition(forward);
        }        


        void Rotate(ref MoveComponent move, ref InputMoveDirectionEvent input)
        {
            var verticalRot = input.Horizontal * move.RotateSpeed;

            var rotation = move.RB.transform.rotation
                * Quaternion.Euler(0f, verticalRot, 0f);

            move.RB.MoveRotation(rotation);
        }
    }
}
