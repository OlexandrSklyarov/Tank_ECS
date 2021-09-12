using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public class MoveSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<MoveComponent, InputMoveDirectionEvent, VehicleComponent> filter;

        #endregion


        public void Run()
        {
            foreach (var id in filter)
            {
                ref var moveComponent = ref filter.Get1(id);
                var input = filter.Get2(id);
                var engine = filter.Get3(id);

                Rotate(ref moveComponent, input, engine);
                MoveForce(ref moveComponent, input, engine);
            }
        }        


        void MoveForce(ref MoveComponent move, InputMoveDirectionEvent input, VehicleComponent engine)
        {
            var acceleration = 0f;

            if (!move.IsGrounded) return;
            
                bool isPresetMove = Mathf.Abs(input.Vertical) > 0f;

                acceleration = input.Vertical * engine.MovePower;

                foreach (var wheel in move.Wheels)
                {
                    acceleration = (wheel.rpm < engine.MaxWheelRPM) ? acceleration : 0f; 

                    if (isPresetMove)
                    {
                        wheel.brakeTorque = 0f;
                        wheel.motorTorque = acceleration;  
                    }
                    else
                    {
                        wheel.brakeTorque = engine.MovePower;
                    }
                }    
                    
        }


        void Rotate(ref MoveComponent move, InputMoveDirectionEvent input, VehicleComponent engine)
        {
            
            
            var verticalRot = input.Horizontal * engine.RotateSpeed;

            Quaternion rotation;

            if (input.Horizontal != 0f)
            {
                rotation = move.TR.rotation * Quaternion.Euler(0f, verticalRot, 0f);
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
