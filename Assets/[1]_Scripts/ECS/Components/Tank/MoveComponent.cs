using UnityEngine;

namespace SA.Tanks
{
    public struct MoveComponent
    {
        public Vector2 Direction { get; set; }
        public float MoveSpeed { get; set; }
        public float RotateSpeed { get; set; }   
        public float SpeedMultiplayer => SPEED_MULTIPLAYER;    
        public Rigidbody RB { get; set; }       

        const float SPEED_MULTIPLAYER = 0.02f; 
    }
}