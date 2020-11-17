using Leopotam.Ecs;
using SA.Tanks.Data;
using UnityEngine;
using UnityEngine.AI;

namespace SA.Tanks.Services
{
    public class PlayerTankBuilder : BaseTankBuilder
    {       
         public virtual void Setup(EcsWorld world, DataGame dataGame, Camera mainCamera, GamePool pool)
        {
            this.world = world;
            dataTank = dataGame.PlayerTank;
            weapon = dataGame.SimpleTankWeapon;
            this.mainCamera = mainCamera;
            this.pool = pool;               
        }

        
        public override void SetUnitComponent()
        {
            var tr = poolGO.PoolTransform;

            //добавляем компонент игрока
            var pivot = new GameObject("Pivot");
            pivot.transform.SetParent(tr);
            pivot.transform.localPosition = dataTank.CameraPivotPosition;
            entity.Replace(new PlayerComponent()
            {
                TankType = dataTank.TankType,
                RootTransform = tr,
                Pivot = pivot.transform
            });
        }


        public override void SetMoveComponent()
        {
            base.SetMoveComponent();
            poolGO.PoolTransform.GetComponent<NavMeshAgent>().enabled = false;
        }


        public override void SetBraineComponent()
        {
            Debug.Log("Brain => player control!!!");            
        }
    }
}