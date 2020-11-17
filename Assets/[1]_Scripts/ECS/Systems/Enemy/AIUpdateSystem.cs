using Leopotam.Ecs;

namespace SA.Tanks
{
    public struct AIUpdateSystem : IEcsRunSystem
    {
        readonly EcsFilter<EnemyComponent, BrainAIComponent> aiFilter;


        public void Run()
        {    
            foreach (var id in aiFilter)
            {                
                ref var entity = ref aiFilter.GetEntity(id);
                ref var brain = ref aiFilter.Get2(id);

                var curState = brain.CurrentState;

                curState.UpdateState(ref brain, ref entity);
            }
        }
    }
}