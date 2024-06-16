using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class FuelProductionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FuelProductionComponent, ResourceComponent> _fuelFilter = null;

        public void Run()
        {
            foreach (var i in _fuelFilter)
            {
                ref var fuelComponent = ref _fuelFilter.Get1(i);
                ref var resourceComponent = ref _fuelFilter.Get2(i);

                if (fuelComponent.isProductionFuel && 
                    resourceComponent.currentMaterials > 0 && 
                    resourceComponent.currentElectricity > 0)
                {
                    if (resourceComponent.currentFuel < resourceComponent.maxFuel)
                    {
                        float materialsToConvert = Math.Min(resourceComponent.currentMaterials, fuelComponent.fuelProductionSpeed);
                        materialsToConvert = Math.Min(materialsToConvert, resourceComponent.currentElectricity); // Только если электричества достаточно
                        materialsToConvert = Math.Min(materialsToConvert, resourceComponent.maxFuel - resourceComponent.currentFuel); // Не конвертировать больше, чем максимальное значение топлива
                        resourceComponent.currentMaterials -= materialsToConvert * Time.deltaTime;
                        resourceComponent.currentElectricity -= materialsToConvert  * Time.deltaTime; // Уменьшаем количество электричества на количество материалов, потраченных на производство топлива
                        resourceComponent.currentFuel += materialsToConvert  * Time.deltaTime;
                    }
                    else
                    {
                        fuelComponent.isProductionFuel = false; // Производство прекращается, если топлива больше максимального значения
                    }
                }
                else
                {
                    fuelComponent.isProductionFuel = false; // Производство прекращается, если материалы или электричество заканчиваются
                }
            }
        }
    }
}
