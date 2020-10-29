using UnityEngine;

namespace SA.Tanks
{
    public  struct WaponComponent
    {
        public Transform FirePoint { get; set; }
        public float ShellSpeed { get; set; }
        public int Damage { get; set; }
        public float ReloadTime { get; set; }
        public float LastFireTime { get; set; }
    }
}