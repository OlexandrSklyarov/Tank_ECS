using Leopotam.Ecs;
using UnityEngine;

namespace SA.Tanks
{
    public struct PlayerInputSystem : IEcsRunSystem
    {
        #region Var

        readonly EcsFilter<InputEventComponent> inputEventFilter;

        readonly Camera mainCamera;

        const string HORIZONTAL = "Horizontal";
        const string VERTICAL = "Vertical";

        const float MAX_RAY_DISTANCE = 500f;

        #endregion


        public void Run()
        {
            var x = Input.GetAxis(HORIZONTAL);
            var y = Input.GetAxis(VERTICAL);
            var isLeftMouseDown = Input.GetMouseButtonDown(0);
            var isRightMousePressed = Input.GetMouseButton(1);
            var mousePosition = Input.mousePosition;

            foreach (var id in inputEventFilter)
            {
                ref var inputEvent = ref inputEventFilter.Get1(id);
                var playerEntity = inputEventFilter.GetEntity(id);

                inputEvent.Direction = new Vector2(x, y);
                inputEvent.AimPosition = Vector3.zero;

                //если нажата п.к.м
                if (isRightMousePressed)
                {
                    var ray = mainCamera.ScreenPointToRay(mousePosition);

                    //если луч что либо пересёк, устанавливаем точку прицеливания
                    if (Physics.Raycast(ray, out RaycastHit hit, MAX_RAY_DISTANCE))
                    {
                        inputEvent.AimPosition = hit.point;
                    }

                    if (isLeftMouseDown)
                    {
                        playerEntity.Get<ShootingEvent>();
                    }
                }



            }
        }
    }
}
