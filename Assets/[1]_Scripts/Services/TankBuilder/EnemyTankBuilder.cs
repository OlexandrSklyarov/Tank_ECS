using Leopotam.Ecs;
using SA.Tanks.Data;
using UnityEngine;
using UnityEngine.AI;

namespace SA.Tanks.Services
{
    public class EnemyTankBuilder : BaseTankBuilder
    {        
        #region Var

        Transform[] waitpoints;

        #endregion


        public void Setup(EcsWorld world, DataGame dataGame, Camera mainCamera, GamePool pool, Transform[] waitpoints)
        {
             this.world = world;
            dataTank = GetRandomEnemy(dataGame);
            weapon = dataGame.SimpleTankWeapon;
            this.mainCamera = mainCamera;
            this.pool = pool;
            this.waitpoints = waitpoints;
           
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

        
        public override void SetBraineComponent()
        {
            var stateController = poolGO.PoolTransform.GetComponent<StateController>();
            stateController.SetupAI(true, waitpoints);

            entity.Replace(new BrainAIComponent()
            {
                Agent = poolGO.PoolTransform.GetComponent<NavMeshAgent>(),
                StateController = stateController
            });
        }
    }
}