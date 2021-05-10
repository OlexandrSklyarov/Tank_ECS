using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public class CheckGroundSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<MoveComponent, VehicleComponent> filter;

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
            // var origin = move.RB.transform;
            // var groundLayer = move.GroundLayer;
            // var duration = 1f;
            // var offset = 1.3f;

            // var upPoints = GetUpPoints(origin, duration);
            // var downPoints = GetDownPoints(origin, upPoints, duration * offset * -1f);

            // for (var i = 0; i < upPoints.Length; i++)
            // {
            //     var up = upPoints[i];
            //     var down = downPoints[i];
            //     var ray = new Ray(up, down);

            //     Debug.DrawRay(up, down - up, Color.red, duration * offset);

            //     var result = Physics.RaycastNonAlloc(ray, move.Hits, duration * offset);

            //     if (result > 0)
            //     {
            //         Debug.Log($"hit count: {result}");
            //         move.IsGrounded = true;
            //         return;
            //     }
            // }

            // Debug.Log("No ground...");

            // move.IsGrounded = false;

            move.IsGrounded = true;
        }


        Vector3[] GetUpPoints(Transform origin, float duration)
        {
            return new Vector3[]
            {
                new Vector3(origin.localPosition.x - 1, origin.localPosition.y + duration, origin.localPosition.z + 1), //forward left center
                new Vector3(origin.localPosition.x + 1, origin.localPosition.y + duration, origin.localPosition.z + 1), //forward right center

                new Vector3(origin.localPosition.x - 1, origin.localPosition.y + duration, origin.localPosition.z), //left center
                new Vector3(origin.localPosition.x + 1, origin.localPosition.y + duration, origin.localPosition.z), //right center

                new Vector3(origin.localPosition.x - 1, origin.localPosition.y + duration, origin.localPosition.z - 1), //back left center
                new Vector3(origin.localPosition.x + 1, origin.localPosition.y + duration, origin.localPosition.z - 1) //back right center
            };
        }


        Vector3[] GetDownPoints(Transform origin, Vector3[] upPoints, float duration)
        {
            var points = new Vector3[upPoints.Length];

            for (int v = 0; v < points.Length; v++)
            {
                points[v] = upPoints[v] + origin.up * duration;
            };

            return points;
        }

    }
}
