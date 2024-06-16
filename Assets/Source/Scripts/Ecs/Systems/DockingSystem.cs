// using Leopotam.Ecs;
// using UnityEngine;
//
// namespace Ecs
// {
//     sealed class DockingSystem : IEcsRunSystem
//     {
//         private readonly EcsFilter<DockingComponent, UIComponent> dockingFilter = null;
//
//         public void Run()
//         {
//             foreach (var i in dockingFilter)
//             {
//                 ref var dockingComponent = ref dockingFilter.Get1(i);
//                 ref var uiComponent = ref dockingFilter.Get2(i);
//
//                 if (Input.GetKeyDown(KeyCode.P) && dockingComponent.dockedTo != null)
//                 {
//                     var rb = dockingComponent.dockedTo.GetComponent<Rigidbody>();
//                     if (rb == null)
//                     {
//                         Debug.LogError("Docked object does not have a Rigidbody.");
//                         continue;
//                     }
//
//                     if (!dockingComponent.isDocked)
//                     {
//                         rb.isKinematic = true;
//                         dockingComponent.isDocked = true;
//                         dockingComponent.dockedEntityId = dockingFilter.GetEntity(i).GetInternalId();
//                         uiComponent.dockText.text = "Press P to undock";
//                         uiComponent.statMenu.SetActive(true);
//
//                         ShowDockedStatMenu(dockingComponent.dockedEntityId);
//                     }
//                     else
//                     {
//                         rb.isKinematic = false;
//                         dockingComponent.isDocked = false;
//                         uiComponent.dockText.text = "Press P to dock";
//                         uiComponent.statMenu.SetActive(false);
//
//                         HideDockedStatMenu(dockingComponent.dockedEntityId);
//                         dockingComponent.dockedEntityId = -1;
//                     }
//                 }
//             }
//         }
//
//         private void ShowDockedStatMenu(int dockedEntityId)
//         {
//             foreach (var j in dockingFilter)
//             {
//                 if (dockingFilter.GetEntity(j).GetInternalId() == dockedEntityId)
//                 {
//                     ref var dockedUIComponent = ref dockingFilter.Get2(j);
//                     dockedUIComponent.statMenu.SetActive(true);
//                 }
//             }
//         }
//
//         private void HideDockedStatMenu(int dockedEntityId)
//         {
//             foreach (var j in dockingFilter)
//             {
//                 if (dockingFilter.GetEntity(j).GetInternalId() == dockedEntityId)
//                 {
//                     ref var dockedUIComponent = ref dockingFilter.Get2(j);
//                     dockedUIComponent.statMenu.SetActive(false);
//                 }
//             }
//         }
//     }
// }
