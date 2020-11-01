using UnityEngine;

namespace SA.Tanks.Services
{
    public interface ITankBuilder
    {
        void Create(Vector3 pos, Quaternion rot);
        void Reset();
        void SetUnitComponent();
        void SetPoolObjectComponent();
        void SetHealthComponent();
        void SetUIComponent();
        void SetAimComponent();
        void SetMoveComponent();
        void SetTurretComponent();
        void SetWeaponComponent();   
        void SetBraineComponent();
    }
}