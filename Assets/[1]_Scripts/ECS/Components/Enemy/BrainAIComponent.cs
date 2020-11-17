using UnityEngine;
using UnityEngine.AI;
using SA.Tanks.FSM;

namespace SA.Tanks
{    
    public struct BrainAIComponent
    {
        #region Properties

        public NavMeshAgent Agent {get; set;}   

        public Transform[] Waitpoints {get; set;}
        public Transform Eyes {get; set;}
        public Transform ChaseTarget {get; set;}  

        public StateFSM CurrentState {get; set;}
        public StateFSM RemainState {get; set;}
        public BotStats EnemyStats {get; set;}  

        public LayerMask PlayerLayer {get; set;}
        
        public int NextWayPoint {get; set;}
        public float StateTimeElapsed {get; private set;}  

        #endregion


        #region Helper Methods

        public void TransitionToState(StateFSM nextState)
        {
            if (nextState != RemainState)
            {       
                if (nextState != CurrentState) 
                    OnExitState();  

                CurrentState = nextState;            
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

        #endregion
    }
}