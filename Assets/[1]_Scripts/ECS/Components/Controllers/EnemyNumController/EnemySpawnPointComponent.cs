using UnityEngine;

namespace SA.Tanks
{
    public struct EnemySpawnPointComponent
    {
        public int FreeSpawnPointIndex 
        {
            get => currentSpawnPointIndex;
            set { currentSpawnPointIndex = (value > MaxIndex) ? 0 : value; } 
        }

        public int MaxIndex { private get; set; }
      
        int currentSpawnPointIndex;
    }
}
