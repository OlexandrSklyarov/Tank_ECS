using UnityEngine;

namespace SA.Tanks
{
    public struct EnemySpawnPointComponent
    {
        public Transform[] AllSpawnPoints { get; set; }
        public Transform FreeSpawnPoint { get; set; }        
    }
}
