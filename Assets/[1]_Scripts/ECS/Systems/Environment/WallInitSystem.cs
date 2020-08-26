using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct WallInitSystem : IEcsPreInitSystem
    {
        readonly EcsWorld _world;
        readonly GameObject[] walls;

        public void PreInit()
        {
            for(int i = 0; i < walls.Length; i++) 
            {
                var entity = _world.NewEntity();
                entity.Replace(new WallComponent() { ViewGO = walls[i]});
            }
        }
    }
}
