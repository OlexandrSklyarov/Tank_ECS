using UnityEngine;
using UnityEngine.AI;
using Leopotam.Ecs;

public class StateController : MonoBehaviour
{
    #region Properties
    
    public NavMeshAgent NavMeshAgent { get; private set; }
    public EcsEntity Entity { get; private set; }
    public Transform[] Waitpoints { get; private set; }
    public float StateTimeElapsed { get; private set; }
    
    #endregion


    #region Var    

    public State currentState;
    public State remainState;
    public EnemyStats enemyStats;
    public Transform eyes;
    public LayerMask playerLayer;

    
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    

    bool isAIAction;

    #endregion


    void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }


    public void SetupAI(bool isAIAction, Transform[] wayPointList, ref EcsEntity entity)
    {        
        this.isAIAction = isAIAction;
        NavMeshAgent.enabled = isAIAction;

        Waitpoints = wayPointList;
        Entity = entity;        
    }


    public void Tick()
    {
        if (isAIAction)
        {
            currentState.UpdateState(this);
        }            
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
            if (nextState != currentState) 
                OnExitState();  

            currentState = nextState;            
        }
    }


    public bool IsCheckCountDownElapsed(float duration)
    {
        StateTimeElapsed += Time.deltaTime;
        return (StateTimeElapsed >= duration);
    }


    void OnExitState()
    {
        StateTimeElapsed = 0f;
    }

}