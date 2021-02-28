using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks.FSM
{
    [System.Serializable]
    public class TransitionFSM
    {
        public DecisionFSM Decision;
        public StateFSM TrueState;
        public StateFSM FalseState;
    }
}