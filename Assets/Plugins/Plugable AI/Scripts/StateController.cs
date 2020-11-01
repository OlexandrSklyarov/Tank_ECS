using UnityEngine;
using UnityEngine.AI;
using SA.TankShooting;

public class StateController : MonoBehaviour
{
    #region Var

    public State currentState;
    public State remainState;
    public EnemyStats enemyStats;
    public Transform eyes;
    public LayerMask playerLayer;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public TankShooting tankShooting;
    [HideInInspector] public Transform[] waitpoints;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    bool isAIAction;

    #endregion


    void Awake()
    {
        tankShooting = GetComponent<TankShooting>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    public void SetupAI(bool isAIAction, Transform[] wayPointList)
    {
        this.waitpoints = wayPointList;
        this.isAIAction = isAIAction;

        navMeshAgent.enabled = isAIAction;
    }


    public void Tick()
    {
        if (isAIAction)
            currentState.UpdateState(this);
    }


    void OnDrawGizmos()
    {
        if (currentState && eyes)
        {
            Gizmos.color = currentState.scenesGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.LookSphereCastRadius);
        }
    }


    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }


    public bool IsCheckCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }


    void OnExitState()
    {
        stateTimeElapsed = 0f;
    }

}
