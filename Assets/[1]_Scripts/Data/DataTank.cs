using UnityEngine;
using SA.Tanks.FSM;

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
        public BotStats BotStats => botStats;
        public StateFSM StartState => startState;
        public StateFSM RemainState => remainState;
        public LayerMask PlayerLayer => playerLayer;

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

        [Space]
        [Header("FSM")]
        [SerializeField] BotStats botStats;
        [SerializeField] StateFSM startState;
        [SerializeField] StateFSM remainState;
        [SerializeField] LayerMask playerLayer;

        #endregion

    }


    public enum TankType
    {
        TANK_GREEN, TANK_RED, TANK_PURPLE
    }
}

