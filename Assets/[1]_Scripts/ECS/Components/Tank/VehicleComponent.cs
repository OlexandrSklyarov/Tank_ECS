using System;

namespace SA.Tanks
{
    public struct VehicleComponent
    {
        public float MovePower { get; set; }
        public float RotateSpeed { get; set; }
        public float MaxWheelRPM => 500f;



    }
}
