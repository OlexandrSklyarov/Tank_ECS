using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Data/Tank/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float LookSphereCastRadius => lookSphereCastRadius;
    public float LookRange => lookRange;
    public float AttackRange => attackRange;
    public float AttackRate => attackRate;
    public float AttackForce => attackForce;
    public float SearchingTurnSpeed => searchingTurnSpeed;
    public float SearchDuration => searchDuration;


    [SerializeField] [Range(1f,100f)] float lookSphereCastRadius = 1f;
    [SerializeField] [Range(1f,100f)] float lookRange = 5f;
    [SerializeField] [Range(1f,100f)] float attackRange = 5f;
    [SerializeField] [Range(1f,100f)] float attackRate = 2f;
    [SerializeField] [Range(1f,100f)] float attackForce = 2f;
    [SerializeField] [Range(1f,100f)] float searchingTurnSpeed = 2f;
    [SerializeField] [Range(1f,100f)] float searchDuration = 5f;
    

}
