using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class ResourceSpendingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ResourceComponent, ResourceSpendingComponent, DirectionComponent, RestartComponent/*, DockingComponent*/> resourceFilter = null;

        public void Run()
        {
            foreach (var i in resourceFilter)
            {
                ref var resourceComponent = ref resourceFilter.Get1(i);
                ref var resourceSpendingComponent = ref resourceFilter.Get2(i);
                ref var directionComponent = ref resourceFilter.Get3(i);
                ref var restartComponent = ref resourceFilter.Get4(i);
                
                // Debug.Log(resourceComponent.currentMaterials);
                // Debug.Log($"Resource entity {i} {resourceComponent.currentFuel}");


                // Генерация электричества (вне зависимости от состояния лазера)
                resourceComponent.currentElectricity += resourceSpendingComponent.electricityGenerationRate * Time.deltaTime;
                if (resourceComponent.currentElectricity > resourceComponent.maxElectricity)
                {
                    resourceComponent.currentElectricity = resourceComponent.maxElectricity;
                }
                
                if (resourceComponent.currentElectricity <= 0)
                {
                    resourceComponent.currentElectricity = 0;
                }
                
                if (resourceComponent.currentFuel <= 0)
                {
                    resourceComponent.currentFuel = 0;
                }

                // Расход электричества лазером
                if (directionComponent.isLazerOn)
                {
                    if (resourceComponent.currentElectricity <= resourceComponent.minElectricityLazer)
                    {
                        directionComponent.isLazerOn = false;
                    }
                    else if (directionComponent.isLazerOn && resourceComponent.currentElectricity > resourceComponent.minElectricityLazer)
                    {
                        resourceComponent.currentElectricity -= resourceSpendingComponent.electricityLazerSpending * Time.deltaTime;
                    }
                }

                if (directionComponent.Direction.y != 0 || 
                    directionComponent.Direction.x != 0 || 
                    directionComponent.Direction.z != 0 || 
                    directionComponent.yaw != 0 || 
                    directionComponent.roll != 0 || 
                    directionComponent.pitch != 0 || 
                    directionComponent.isStabization)
                {
                    resourceComponent.currentFuel -= resourceSpendingComponent.fuelSpending * Time.deltaTime;
                }

                if (resourceComponent.currentFuel <= 0/* && dockingComponent.isDocked == false*/)
                {
                    restartComponent.isRestart = true;
                }
            }
        }
    }
}