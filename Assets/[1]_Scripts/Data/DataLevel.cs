using UnityEngine;

namespace SA.Tanks.Data
{
    [CreateAssetMenu(fileName = "DataLevel", menuName = "Data/DataLevel")]
    public class DataLevel : ScriptableObject
    {
        #region Properties

        public LayerMask GroundLayer => groundLayer;
        public int EnemyCount => enemyCount;
        

        #endregion


        #region  Var

        [SerializeField] LayerMask groundLayer;
        [SerializeField] [Range(1, 500)]int enemyCount = 1;

        #endregion
    }
}
