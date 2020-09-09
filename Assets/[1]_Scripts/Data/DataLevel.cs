using UnityEngine;

namespace SA.Tanks.Data
{
    [CreateAssetMenu(fileName = "DataLevel", menuName = "Data/DataLevel")]
    public class DataLevel : ScriptableObject
    {
        #region Properties

        public GameObject LevelMap => levelMap;
        public int EnemyCount=> enemyCount;

        #endregion


        #region  Var

        [SerializeField] GameObject levelMap;
        [SerializeField] [Range(1, 500)]int enemyCount = 1;

        #endregion
    }
}
