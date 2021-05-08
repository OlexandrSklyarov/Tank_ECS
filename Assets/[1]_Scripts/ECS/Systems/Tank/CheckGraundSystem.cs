using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public class CheckGroundSystem: IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<MoveComponent, TankEngineComponent> filter;

        #endregion


        public void Run()
        {
            foreach (var id in filter)
            {
                ref var moveComponent = ref filter.Get1(id);                
                moveComponent.IsGrounded = IsGrounded(moveComponent.RB.transform);           
            }
        } 

        bool IsGrounded(Transform origin)
        {
            // Vector3[] checkPoints = new[]
            // {
            //     new Vector3(origin.localPosition.x - 1, origin.localPosition.y, origin.localPosition.z + 1), //forward left center
            //     new Vector3(origin.localPosition.x + 1, origin.localPosition.y, origin.localPosition.z + 1), //forward right center

            //     new Vector3(origin.localPosition.x - 1, origin.localPosition.y, origin.localPosition.z), //left center
            //     new Vector3(origin.localPosition.x + 1, origin.localPosition.y, origin.localPosition.z), //right center

            //     new Vector3(origin.localPosition.x - 1, origin.localPosition.y, origin.localPosition.z - 1), //back left center
            //     new Vector3(origin.localPosition.x + 1, origin.localPosition.y, origin.localPosition.z - 1) //back right center
                
            // };

            // RaycastHit hit;
            // var length = 1f;

            // for (var i = 0; i < checkPoints.Length; i++)
            // {
            //     var point = checkPoints[i];
            //     var ray = new Ray(point, point * origin.localPosition.y * -length);

            //     Debug.DrawRay(point, Vector3.down, Color.red, length);
            // }

            return true;
        }
    }
}
