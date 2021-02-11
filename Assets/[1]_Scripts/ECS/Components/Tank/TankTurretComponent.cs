using UnityEngine;

namespace SA.Tanks
{
    public struct TankTurretComponent
    {
        public Transform TurretTransform { get; set; }
        public Transform BarrelOriginTransform { get; set; }
        public float RotateSpeed { get; set; }
    }
}
