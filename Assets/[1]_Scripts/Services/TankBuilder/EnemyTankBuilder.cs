using Leopotam.Ecs;
using SA.Tanks.Data;
using UnityEngine;

namespace SA.Tanks.Services
{
    public class EnemyTankBuilder : BaseTankBuilder
    {        
        public override void Setup(EcsWorld world, DataGame dataGame, Camera mainCamera, GamePool pool)
        {
            base.Setup(world, dataGame, mainCamera, pool);
            
            dataTank = GetRandomEnemy(dataGame);
        }


        DataTank GetRandomEnemy(DataGame data)
        {
            var randomIndex = UnityEngine.Random.Range(0, data.EnemyTank.Length);
            return data.EnemyTank[randomIndex];
        }


        public override void SetUnitComponent()
        {
            entity.Replace(new EnemyComponent()
            { 
                TankType = dataTank.TankType
            });
        }
    }
}