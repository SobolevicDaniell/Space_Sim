using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class CameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraComponent, PlayerTagComponent> cameraFilter = null;
        private readonly EcsFilter<EventCameraSwitch> eventFilter = null;
        private readonly EcsFilter<EventCameraOn> eventOnFilter = null;

        private readonly SceneData _sceneData = null;

        public CameraSystem(SceneData sceneData)
        {
            _sceneData = sceneData;
        }

        public void Run()
        {
            // Проверка на первую инициализацию
            foreach (var i in cameraFilter)
            {
                ref var cameraComponent = ref cameraFilter.Get1(i);
                if (cameraComponent.isFirstTime)
                {
                    HandleCameraSwitch(ref cameraComponent, false); // Просто закрепляем камеру
                    cameraComponent.isFirstTime = false;
                }
            }

            // Обработка события переключения камеры
            foreach (var eventIndex in eventFilter)
            {
                foreach (var i in cameraFilter)
                {
                    ref var playerTagComponent = ref cameraFilter.Get2(i);
                    ref var cameraComponent = ref cameraFilter.Get1(i);

                    if (playerTagComponent.IsControlledByPlayer)
                    {
                        HandleCameraSwitch(ref cameraComponent, true); // Переключение на следующую
                    }
                }

                // Удаляем событие после обработки
                ref var eventEntity = ref eventFilter.GetEntity(eventIndex);
                eventEntity.Del<EventCameraSwitch>();
            }

            // Обработка события включения текущей камеры
            foreach (var eventIndex in eventOnFilter)
            {
                foreach (var i in cameraFilter)
                {
                    ref var playerTagComponent = ref cameraFilter.Get2(i);
                    ref var cameraComponent = ref cameraFilter.Get1(i);

                    if (playerTagComponent.IsControlledByPlayer)
                    {
                        HandleCameraSwitch(ref cameraComponent, false); // Включаем текущую
                    }
                }

                // Удаляем событие после обработки
                ref var eventEntity = ref eventOnFilter.GetEntity(eventIndex); // Исправлено
                eventEntity.Del<EventCameraOn>();
            }
        }

        private void HandleCameraSwitch(ref CameraComponent cameraComponent, bool switchToNext)
        {
            if (switchToNext)
            {
                cameraComponent.currentCameraIndex =
                    (cameraComponent.currentCameraIndex + 1) % cameraComponent.camerasPosition.Length;
            }

            MoveCameraToPosition(cameraComponent);
            Debug.Log($"Камера переключена на позицию {cameraComponent.currentCameraIndex}");
        }

        private void MoveCameraToPosition(CameraComponent cameraComponent)
        {
            if (cameraComponent.currentCameraIndex < cameraComponent.camerasPosition.Length)
            {
                var targetTransform = cameraComponent.camerasPosition[cameraComponent.currentCameraIndex];

                if (targetTransform != null)
                {
                    _sceneData.mainCamera.transform.SetParent(targetTransform.transform);
                    _sceneData.mainCamera.transform.localPosition = Vector3.zero;
                    _sceneData.mainCamera.transform.localRotation = Quaternion.identity;

                    Debug.Log($"Камера перемещена и закреплена в позиции: {targetTransform.name}");
                }
                else
                {
                    Debug.LogWarning("Позиция камеры не указана (null)!");
                }
            }
            else
            {
                Debug.LogError("Индекс позиции камеры выходит за пределы массива!");
            }
        }
    }
}
