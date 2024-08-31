using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    public class UIObjectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UIComponent, ResourceComponent, StabilizationComponent, FuelProductionComponent> uiFilter = null;

        public void Run()
        {
            foreach (var i in uiFilter)
            {
                ref var resourceComponent = ref uiFilter.Get2(i);
                ref var uiComponent = ref uiFilter.Get1(i);
                ref var stabilizationComponent = ref uiFilter.Get3(i);
                ref var fuelComponent = ref uiFilter.Get4(i);

                if (uiComponent.fuelProductionText != null)
                {
                    if (fuelComponent.isProductionFuel)
                    {
                        uiComponent.fuelProductionText.text = "Производство топлива запущено";
                    }
                    else
                    {
                        uiComponent.fuelProductionText.text = "Производство топлива не запущено";
                    }
                }
                
                if (uiComponent.fuelSlider != null)
                {
                    if (resourceComponent.currentFuel > resourceComponent.maxFuel)
                    {
                        resourceComponent.currentFuel = resourceComponent.maxFuel;
                    }
                    uiComponent.fuelSlider.value = resourceComponent.currentFuel / resourceComponent.maxFuel;
                }

                if (uiComponent.electricitySlider != null)
                {
                    if (resourceComponent.currentElectricity > resourceComponent.maxElectricity)
                    {
                        resourceComponent.currentElectricity = resourceComponent.maxElectricity;
                    }
                    uiComponent.electricitySlider.value = resourceComponent.currentElectricity / resourceComponent.maxElectricity;
                }

                if (uiComponent.materialsText != null)
                {
                    if (resourceComponent.currentMaterials > resourceComponent.maxMaterials)
                    {
                        resourceComponent.currentMaterials = resourceComponent.maxMaterials;
                    }
                    float currentMaterialsRounded = (float)Math.Round(resourceComponent.currentMaterials, 1);
                    float maxMaterialsRounded = (float)Math.Round(resourceComponent.maxMaterials, 1);
                    uiComponent.materialsText.text = $"{currentMaterialsRounded} / {maxMaterialsRounded}";
                }


                if (uiComponent.stabText != null)
                {
                    if (stabilizationComponent.isStabization)
                    {
                        uiComponent.stabText.text = "Стабилизатор Вкл";
                    }
                    else
                    {
                        uiComponent.stabText.text = "Стабилизатор Выкл";
                    }
                }
            }
        }
    }
}
