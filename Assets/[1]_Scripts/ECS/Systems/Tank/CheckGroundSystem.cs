using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public class CheckGroundSystem: IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<MoveComponent, VehicleComponent> filter;

        RaycastHit[] hits = new RaycastHit[6];

        #endregion


        public void Run()
        {
            foreach (var id in filter)
            {
                ref var move = ref filter.Get1(id);                
                CheckGround(ref move);           
            }
        } 

        void CheckGround(ref MoveComponent move)
        {
            var origin = move.RB.transform;
            var groundLayer = move.GroundLayer;
            var duration = 0.2f;

            Vector3[] upPoints = new[]
            {
                new Vector3(origin.localPosition.x - 1, origin.localPosition.y + duration, origin.localPosition.z + 1), //forward left center
                new Vector3(origin.localPosition.x + 1, origin.localPosition.y + duration, origin.localPosition.z + 1), //forward right center

                new Vector3(origin.localPosition.x - 1, origin.localPosition.y + duration, origin.localPosition.z), //left center
                new Vector3(origin.localPosition.x + 1, origin.localPosition.y + duration, origin.localPosition.z), //right center

                new Vector3(origin.localPosition.x - 1, origin.localPosition.y + duration, origin.localPosition.z - 1), //back left center
                new Vector3(origin.localPosition.x + 1, origin.localPosition.y + duration, origin.localPosition.z - 1) //back right center
                
            };

            var downPoints = new Vector3[upPoints.Length];            

            for(int v = 0; v < downPoints.Length; v++)
            {
                downPoints[v] = upPoints[v] + origin.up * -(duration * 2); 
            };            

            for (var i = 0; i < upPoints.Length; i++)
            {
                var up = upPoints[i];
                var down = downPoints[i];
                var ray = new Ray(up, down);
                
                Debug.DrawRay(up, (down - up).normalized, Color.red, duration * 2.5f);

                var result = Physics.RaycastNonAlloc(ray, move.Hits, duration * 2.5f, groundLayer);
                
                if (result > 0) 
                {
                    move.IsGrounded = true;
                    return;
                }                
            }

            Debug.Log("No ground...");

            move.IsGrounded = false;
        }
    }
}
