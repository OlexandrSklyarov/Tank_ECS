using SA.Tanks.Data;
using UnityEngine;

namespace SA.Tanks
{
    public struct PlayerComponent
    {
        public TankType TankType { get; set; }
        public Transform RootTransform { get; set; }
        public Transform Pivot { get; set; }
    }
}
