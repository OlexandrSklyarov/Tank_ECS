using UnityEngine;

namespace SA.Tanks.Data
{
    [CreateAssetMenu(fileName = "DataTank", menuName = "Data/DataTank")]
    public class DataTank : ScriptableObject
    {
        #region Properties

        public TankType TankType => tankType;
        public int HP => hp;
        public int MaxHP => maxHp;
        public float MoveSpeed => moveSpeed;
        public float RotateSpeed => rotateSpeed;
        public float MaxAngularVelosity => 100f;
        public Vector3 CameraPivotPosition => cameraPivotPosition;
        public float TurretSpeedRotate => turretSpeedRotate;
        public float TurretMinDistance => turretMinDistance;

        #endregion


        #region Var

        [Header("Tank")]
        [SerializeField] TankType tankType;
        [SerializeField] [Range(1, 1000)] int hp;
        [SerializeField] [Range(1, 1000)] int maxHp;
        [SerializeField] [Range(1f, 100f)] float moveSpeed;
        [SerializeField] [Range(1f, 100f)] float rotateSpeed;
        [SerializeField] Vector3 cameraPivotPosition;

        [Space]
        [Header("Turret")]
        [SerializeField] [Range(1f, 15f)] float turretSpeedRotate;
        [SerializeField] [Range(1f, 15f)] float turretMinDistance;

        #endregion

    }


    public enum TankType
    {
        TANK_1, TANK_2
    }
}

