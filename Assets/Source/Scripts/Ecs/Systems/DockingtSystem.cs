using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class DockingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DockingComponent, DirectionComponent, ModelComponent, MovebleComponent, UIComponent> _dockingFilter = null;
        
        public void Run()
        {
            foreach (var i in _dockingFilter)
            {
                ref var dockingComponent = ref _dockingFilter.Get1(i);
                ref var directionComponent = ref _dockingFilter.Get2(i);
                ref var modelComponent = ref _dockingFilter.Get3(i);
                ref var movebleComponent = ref _dockingFilter.Get4(i);
                ref var uiComponent = ref _dockingFilter.Get5(i);

                if (directionComponent.isDocking && !dockingComponent.isDocked && dockingComponent.readyToInteract)
                {
                    Dock(ref dockingComponent, ref modelComponent, ref movebleComponent, ref directionComponent, ref uiComponent);
                    directionComponent.isDocking = false;  // Сбрасываем флаг после успешного выполнения стыковки
                }
                else if (directionComponent.isDocking && dockingComponent.isDocked)
                {
                    Undock(ref dockingComponent, ref modelComponent, ref movebleComponent, ref uiComponent);
                    directionComponent.isDocking = false;  // Сбрасываем флаг после успешного выполнения разъединения
                }

                if (dockingComponent.isDocked)
                {
                    uiComponent.statMenu.SetActive(true);
                    uiComponent.otherStatMenu.SetActive(true);
                }
            }
        }

        private void Dock(ref DockingComponent dockingComponent, ref ModelComponent modelComponent, ref MovebleComponent movebleComponent, ref DirectionComponent directionComponent, ref UIComponent uiComponent)
        {
            modelComponent.modelTransform.SetParent(dockingComponent.gameObjectWithDock.transform);
            dockingComponent.isDocked = true;
            movebleComponent.rigidbody.isKinematic = true;
            dockingComponent.transferMenu.SetActive(true);
            uiComponent.statMenu.SetActive(true); // Включаем statMenu при стыковке
            uiComponent.otherStatMenu.SetActive(true); // Включаем statMenu при стыковке
            uiComponent.dockText.text = "Для отстыковки нажмите P";
            Debug.Log("Docked");
        }

        private void Undock(ref DockingComponent dockingComponent, ref ModelComponent modelComponent, ref MovebleComponent movebleComponent, ref UIComponent uiComponent)
        {
            modelComponent.modelTransform.SetParent(null);
            dockingComponent.isDocked = false;
            movebleComponent.rigidbody.isKinematic = false;
            dockingComponent.transferMenu.SetActive(false);
            // uiComponent.statMenu.SetActive(false); // Выключаем statMenu при разъединении
            uiComponent.otherStatMenu.SetActive(false); // Выключаем statMenu при разъединении
            Debug.Log("Undocked");
        }
    }
}
