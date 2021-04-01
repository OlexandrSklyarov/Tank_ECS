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
                var input = moveFilter.Get2(id);

                Move(ref moveComponent, input);
                Rotate(ref moveComponent, input);                
            }
        }


        void Move(ref MoveComponent move,  InputMoveDirectionEvent input)
        {
            var forward = move.RB.transform.position;
            
            if (input.Vertical != 0f)
            {
                forward = move.RB.transform.position + 
                    move.RB.transform.forward * 
                    input.Vertical *
                    move.MoveSpeed *
                    move.SpeedMultiplayer;
            }    
            else
            {
                move.RB.velocity = new Vector3
                {
                    x = 0f,
                    y = move.RB.velocity.y,
                    z = 0f
                };
            }        

            //Move
            move.RB.MovePosition(forward);

            Debug.Log($"move rb: {move.RB.velocity}");
        }        


        void Rotate(ref MoveComponent move, InputMoveDirectionEvent input)
        {
            var verticalRot = input.Horizontal * move.RotateSpeed;

            Quaternion rotation;
             
            if (input.Horizontal != 0f)
            {
                rotation = move.RB.transform.rotation * Quaternion.Euler(0f, verticalRot, 0f);
            }
            else
            {
                rotation = move.RB.rotation;
                move.RB.angularVelocity = Vector3.zero;
            }

            move.RB.MoveRotation(rotation);
        }
    }
}
