using Leopotam.Ecs;
using SA.Tanks.Data;
using UnityEngine;

namespace SA.Tanks.Services
{
    public class PlayerTankBuilder : BaseTankBuilder
    {       
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
    }
}