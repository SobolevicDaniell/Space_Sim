using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class ControlSwitchSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<CameraComponent, PlayerTagComponent, DirectionComponent, UIComponent/*, DockingComponent*/> controlSwitchFilter = null;
        private int currentPlayerIndex;

        public void Init()
        {
            // Debug.Log(controlSwitchFilter.GetEntitiesCount());

            // Ищем начального игрока, если он установлен
            for (int i = 0; i < controlSwitchFilter.GetEntitiesCount(); i++)
            {
                ref var playerTagComponent = ref controlSwitchFilter.Get2(i);
                if (playerTagComponent.IsControlledByPlayer)
                {
                    currentPlayerIndex = i;
                    ActivateCamera(ref controlSwitchFilter.Get1(i), true);
                    ActivateStatMenu(ref controlSwitchFilter.Get4(i), true);
                }
                else
                {
                    ActivateCamera(ref controlSwitchFilter.Get1(i), false);
                    ActivateStatMenu(ref controlSwitchFilter.Get4(i), false);
                }
            }
        }

        public void Run()
        {
            foreach (var i in controlSwitchFilter)
            {
                ref var directionComponent = ref controlSwitchFilter.Get3(i);
                // ref var dockingComponent = ref controlSwitchFilter.Get5(i);

                if (directionComponent.isSwitchingControl)
                {
                    SwitchControl();
                    directionComponent.isSwitchingControl = false;
                    break; // Прекращаем итерацию после обнаружения переключения
                }
            }
        }

        private void SwitchControl()
        {
            if (controlSwitchFilter.GetEntitiesCount() == 0)
            {
                return;
            }

            // Отключаем текущую управляемую сущность
            ref var currentPlayerTagComponent = ref controlSwitchFilter.Get2(currentPlayerIndex);
            ref var currentCameraComponent = ref controlSwitchFilter.Get1(currentPlayerIndex);
            ref var currentUIComponent = ref controlSwitchFilter.Get4(currentPlayerIndex);

            currentPlayerTagComponent.IsControlledByPlayer = false;
            ActivateCamera(ref currentCameraComponent, false);
            
            ActivateStatMenu(ref currentUIComponent, false);
            

            // Переход к следующей сущности
            currentPlayerIndex = (currentPlayerIndex + 1) % controlSwitchFilter.GetEntitiesCount();

            // Включаем следующую сущность
            ref var nextPlayerTagComponent = ref controlSwitchFilter.Get2(currentPlayerIndex);
            ref var nextCameraComponent = ref controlSwitchFilter.Get1(currentPlayerIndex);
            ref var nextUIComponent = ref controlSwitchFilter.Get4(currentPlayerIndex);

            nextPlayerTagComponent.IsControlledByPlayer = true;
            ActivateCamera(ref nextCameraComponent, true);
            
            ActivateStatMenu(ref nextUIComponent, true);
            
            
            
            
        }

        private void ActivateCamera(ref CameraComponent cameraComponent, bool isActive)
        {
            if (cameraComponent.cameras != null && cameraComponent.cameras.Length > 0)
            {
                for (int i = 0; i < cameraComponent.cameras.Length; i++)
                {
                    cameraComponent.cameras[i].SetActive(isActive && i == 0); // Включаем только первую камеру
                }
            }
        }

        private void ActivateStatMenu(ref UIComponent uiComponent, bool isActive)
        {
            if (uiComponent.statMenu != null)
            {
                uiComponent.statMenu.SetActive(isActive);
            }
        }
    }
}
