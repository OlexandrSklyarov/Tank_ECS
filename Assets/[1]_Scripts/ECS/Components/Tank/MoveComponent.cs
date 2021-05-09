using UnityEngine;

namespace SA.Tanks
{
    public struct MoveComponent
    {
        public Rigidbody RB { get; set; }
        public LayerMask GroundLayer {get; set; }
        public RaycastHit[] Hits {get; set;}
        public bool IsGrounded { get; set; }
    }
}