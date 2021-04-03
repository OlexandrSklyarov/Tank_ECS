using UnityEngine;

namespace SA.Tanks
{
    public struct TankTurretComponent
    {
        public Transform TurretTransform { get; set; }
        public Transform BarrelOriginTransform { get; set; }
        public float RotateSpeed { get; set; }
        public float MaxDistanceToTarget => MAX_DIST_TO_TARGET;
        public float MaxBarrelRotate => MAX_BARREL_ROTATE;

        const float MAX_DIST_TO_TARGET = 4f;
        const float MAX_BARREL_ROTATE = 0.25f;
    }
}
