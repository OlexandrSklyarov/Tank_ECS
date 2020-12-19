using UnityEngine;

namespace SA.Tanks
{
    public struct TankTurretComponent
    {
        public Transform TurretTransform { get; set; }
        public Transform BarrelTransform { get; set; }
        public Vector3 Target { get; set; }
        public float RotateSpeed { get; set; }
        public float MinDistance { get; set; }
    }
}
