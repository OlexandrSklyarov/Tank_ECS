using UnityEngine;

namespace SA.Tanks
{
    public struct MoveComponent
    {
        public Rigidbody RB { get; set; }
        public bool IsGrounded { get; set; }
    }
}