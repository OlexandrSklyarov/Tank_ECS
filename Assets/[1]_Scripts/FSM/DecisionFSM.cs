using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks.FSM
{   
    public abstract class DecisionFSM : ScriptableObject
    {
        public abstract bool Decide(ref BrainAIComponent brain);    
    }
}