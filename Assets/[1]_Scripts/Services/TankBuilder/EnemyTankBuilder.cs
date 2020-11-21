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


        public override void SetMoveComponent()
        {
            base.SetMoveComponent();
            poolGO.PoolTransform.GetComponent<Rigidbody>().isKinematic = true;
        }

        
        public override void SetBraineComponent()
        {
            var provider = poolGO.PoolTransform.GetComponent<TankProvider>();

            entity.Replace(new BrainAIComponent()
            {
                Agent = poolGO.PoolTransform.GetComponent<NavMeshAgent>(),
                Waitpoints = waitpoints,
                Eyes = provider.Eyes,
                ChaseTarget = null, 
                CurrentState = dataTank.StartState,
                RemainState = dataTank.RemainState,
                EnemyStats = dataTank.BotStats,
                PlayerLayer = dataTank.PlayerLayer
            });
        }
    }
}