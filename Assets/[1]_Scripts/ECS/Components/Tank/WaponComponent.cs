using UnityEngine;

namespace SA.Tanks
{
    public  struct WaponComponent
    {
        public Transform FirePoint { get; set; }
        public float ShellSpeed { get; set; }
        public int Damage { get; set; }
    }
}