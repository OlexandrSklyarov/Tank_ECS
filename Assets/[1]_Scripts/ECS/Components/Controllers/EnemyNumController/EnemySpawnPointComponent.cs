using UnityEngine;

namespace SA.Tanks
{
    public struct EnemySpawnPointComponent
    {
        public Transform FreeSpawnPoint { get; set; }
        public Transform[] AllSpawnPoints { get; set; }
    }
}
