using UnityEngine;

namespace SA.Tanks
{
    public struct MoveComponent
    {
        public float MoveSpeed { get; set; }
        public float RotateSpeed { get; set; }
        public Rigidbody RB { get; set; }
    }
}