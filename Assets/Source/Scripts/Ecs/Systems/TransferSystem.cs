// using Leopotam.Ecs;
// using UnityEngine;
//
// namespace Ecs
// {
//     sealed class TransferSystem : IEcsRunSystem
//     {
//         private readonly EcsFilter<TransferComponent, ResourceComponent> _transferFilter = null;
//         private readonly EcsFilter<TransferComponent, DockingComponent> _dockingFilter = null;
//
//         public void Run()
//         {
//             // Обработка запросов на передачу ресурсов
//             foreach (var i in _transferFilter)
//             {
//                 ref var transferComponent = ref _transferFilter.Get1(i);
//                 ref var resourceComponent = ref _transferFilter.Get2(i);
//
//                 if (transferComponent.transferFuelToSoyuz > 0)
//                 {
//                     TransferFuelToSoyuz(transferComponent.transferFuelToSoyuz, resourceComponent);
//                     transferComponent.ransferFuelToSoyuz = 0;
//                 }
//
//                 if (transferComponent.transferElectricityToSoyuz > 0)
//                 {
//                     TransferElectricityToSoyuz(transferComponent.transferElectricityToSoyuz, resourceComponent);
//                     transferComponent.transferElectricityToSoyuz = 0;
//                 }
//
//                 if (transferComponent.transferMaterialsToSoyuz > 0)
//                 {
//                     TransferMaterialsToSoyuz(transferComponent.transferMaterialsToSoyuz, resourceComponent);
//                     transferComponent.transferMaterialsToSoyuz = 0;
//                 }
//
//                 if (transferComponent.transferFuelToStation > 0)
//                 {
//                     TransferFuelToStation(transferComponent.transferFuelToStation, resourceComponent);
//                     transferComponent.transferFuelToStation = 0;
//                 }
//
//                 if (transferComponent.transferElectricityToStation > 0)
//                 {
//                     TransferElectricityToStation(transferComponent.transferElectricityToStation, resourceComponent);
//                     transferComponent.transferElectricityToStation = 0;
//                 }
//
//                 if (transferComponent.transferMaterialsToStation > 0)
//                 {
//                     TransferMaterialsToStation(transferComponent.transferMaterialsToStation, resourceComponent);
//                     transferComponent.transferMaterialsToStation = 0;
//                 }
//             }
//
//             // Проверка стыковки и передача ресурсов
//             foreach (var i in _dockingFilter)
//             {
//                 ref var transferComponent = ref _dockingFilter.Get1(i);
//                 ref var dockingComponent = ref _dockingFilter.Get2(i);
//
//                 if (dockingComponent.isDocked && dockingComponent.readyToInteract)
//                 {
//                     if (dockingComponent.transferMenu.activeSelf)
//                     {
//                         // Активируем transferMenu, поэтому нет необходимости в дополнительной активации statMenu
//                         if (transferComponent.transferMenu.activeSelf != dockingComponent.transferMenu.activeSelf)
//                         {
//                             transferComponent.transferMenu.SetActive(true);
//                         }
//                     }
//                     else
//                     {
//                         // Если transferMenu не активно, то проверяем и активируем statMenu
//                         if (!transferComponent.transferMenu.activeSelf)
//                         {
//                             transferComponent.transferMenu.SetActive(true);
//                             transferComponent.statMenu.SetActive(true);
//                         }
//                     }
//                 }
//                 else
//                 {
//                     // Если мы не стыкуемся, отключаем transferMenu и statMenu
//                     transferComponent.transferMenu.SetActive(false);
//                     transferComponent.statMenu.SetActive(false);
//                 }
//             }
//         }
//
//         private void TransferFuelToSoyuz(float amount, ResourceComponent resourceComponent)
//         {
//             // Проверяем, достаточно ли топлива на станции для передачи
//             if (resourceComponent.currentFuel >= amount)
//             {
//                 // Передаем топливо на союз
//                 var soyuzEntity = FindSoyuzEntity();
//                 if (soyuzEntity.IsNotNullAndAlive())
//                 {
//                     ref var soyuzResourceComponent = ref soyuzEntity.Get<ResourceComponent>();
//                     soyuzResourceComponent.currentFuel += amount;
//
//                     // Уменьшаем количество топлива на станции
//                     resourceComponent.currentFuel -= amount;
//                 }
//             }
//             else
//             {
//                 Debug.Log("Not enough fuel on the station to transfer.");
//             }
//         }
//
//         private void TransferElectricityToSoyuz(float amount, ResourceComponent resourceComponent)
//         {
//             // Проверяем, достаточно ли электричества на станции для передачи
//             if (resourceComponent.currentElectricity >= amount)
//             {
//                 // Передаем электричество на союз
//                 var soyuzEntity = FindSoyuzEntity();
//                 if (soyuzEntity.IsNotNullAndAlive())
//                 {
//                     ref var soyuzResourceComponent = ref soyuzEntity.Get<ResourceComponent>();
//                     soyuzResourceComponent.currentElectricity += amount;
//
//                     // Уменьшаем количество электричества на станции
//                     resourceComponent.currentElectricity -= amount;
//                 }
//             }
//             else
//             {
//                 Debug.Log("Not enough electricity on the station to transfer.");
//             }
//         }
//
//         private void TransferMaterialsToSoyuz(int amount, ResourceComponent resourceComponent)
//         {
//             // Проверяем, достаточно ли материалов на станции для передачи
//             if (resourceComponent.currentMaterials >= amount)
//             {
//                 // Передаем материалы на союз
//                 var soyuzEntity = FindSoyuzEntity();
//                 if (soyuzEntity.IsNotNullAndAlive())
//                 {
//                     ref var soyuzResourceComponent = ref soyuzEntity.Get<ResourceComponent>();
//                     soyuzResourceComponent.currentMaterials += amount;
//
//                     // Уменьшаем количество материалов на станции
//                     resourceComponent.currentMaterials -= amount;
//                 }
//             }
//             else
//             {
//                 Debug.Log("Not enough materials on the station to transfer.");
//             }
//         }
//
//         private void TransferFuelToStation(float amount, ResourceComponent resourceComponent)
//         {
//             // Передаем топливо на станцию
//             resourceComponent.currentFuel += amount;
//         }
//
//         private void TransferElectricityToStation(float amount, ResourceComponent resourceComponent)
//         {
//             // Передаем электричество на станцию
//             resourceComponent.currentElectricity += amount;
//         }
//
//         private void TransferMaterialsToStation(int amount, ResourceComponent resourceComponent)
//         {
//             // Передаем материалы на станцию
//             resourceComponent.currentMaterials += amount;
//         }
//
//         private EcsEntity FindSoyuzEntity()
//         {
//             var filter = _world.Filter<ModelComponent, PlayerTagComponent>();
//             foreach (var i in filter)
//             {
//                 ref var modelComponent = ref filter.Get1(i);
//                 ref var playerTagComponent = ref filter.Get2(i);
//                 if (modelComponent.modelTransform.gameObject.CompareTag("Soyuz") && playerTagComponent.IsControlledByPlayer)
//                 {
//                     return filter.GetEntity(i);
//                 }
//             }
//             return default;
//         }
//     }
// }
