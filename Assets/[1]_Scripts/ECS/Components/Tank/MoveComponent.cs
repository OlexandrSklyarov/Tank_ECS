using UnityEngine;

namespace SA.Tanks
{
    public struct MoveComponent
    {
        public Vector2 Direction { get; set; }
        public float MoveSpeed { get; set; }
        public float RotateSpeed { get; set; }        
        public Rigidbody RB { get; set; }
        public float MinDrag => MIN_DRAG;
        public float MaxDrag => MAX_DRAG;
        public float DragValue => DRAG_VALUE;
        
        
        const float MIN_DRAG = 0f;
        const float MAX_DRAG = 1f;
        const float DRAG_VALUE = 0.1f;
    }
}