using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks.FSM
{
    [CreateAssetMenu(menuName = "Data/FSM/State")]
    public class StateFSM : ScriptableObject
    {
        public Color scenesGizmoColor = Color.gray;
        public ActionFSM[] actions;
        public TransitionFSM[] transitions;


        public void UpdateState(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            DoActions(ref brain, ref entity);
            CheckTransitions(ref brain);
        }


        void DoActions(ref BrainAIComponent brain, ref EcsEntity entity)
        {
            for(int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(ref brain, ref entity);
            }
        }


        void CheckTransitions(ref BrainAIComponent brain)
        {
            for(int i = 0; i < transitions.Length; i++)
            {
                bool decisionSucceded = transitions[i].Decision.Decide(ref brain);

                if (decisionSucceded)
                {
                    brain.TransitionToState(transitions[i].TrueState);
                }
                else
                {
                    brain.TransitionToState(transitions[i].FalseState);
                }
            }
        }
}
}