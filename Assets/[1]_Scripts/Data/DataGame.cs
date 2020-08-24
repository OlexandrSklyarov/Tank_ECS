using UnityEngine;

namespace SA.Tanks.Data
{
    [CreateAssetMenu(fileName = "DataGame", menuName = "Data/DataGame")]
    public class DataGame : ScriptableObject
    {
        #region Properties

        public DataTank PlayerTank => playerTank;
        public DataTank[] EnemyTank => enemyTank;
        public DataWeapon SimpleTankWeapon => simpleTankWeapon;

        #endregion


        #region Var

        [Header("Tanks")]
        [SerializeField] DataTank playerTank;
        [SerializeField] DataTank[] enemyTank;

        [Space]
        [Header("Weapon")]
        [SerializeField] DataWeapon simpleTankWeapon;

        #endregion

    }
}