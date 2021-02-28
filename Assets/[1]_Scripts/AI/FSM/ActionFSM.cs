using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks.FSM
{
    public abstract class ActionFSM: ScriptableObject
    {
        public abstract void Act(ref BrainAIComponent brain, ref EcsEntity entity);    
    }
}