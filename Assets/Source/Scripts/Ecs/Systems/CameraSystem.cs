using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class CameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraComponent, PlayerTagComponent, DirectionComponent> cameraFilter = null;
        private int currentCameraIndex = 0;

        public void Run()
        {
            foreach (var i in cameraFilter)
            {
                ref var directionComponent = ref cameraFilter.Get3(i);

                if (directionComponent.isSwitchingCamera)
                {
                    SwitchCamera();
                    directionComponent.isSwitchingCamera = false;
                }
            }
        }

        public void SwitchCamera()
        {
            foreach (var i in cameraFilter)
            {
                ref var playerTagComponent = ref cameraFilter.Get2(i);
                if (!playerTagComponent.IsControlledByPlayer) continue;

                ref var cameraComponent = ref cameraFilter.Get1(i);
                currentCameraIndex = (currentCameraIndex + 1) % cameraComponent.cameras.Length;

                for (int j = 0; j < cameraComponent.cameras.Length; j++)
                {
                    cameraComponent.cameras[j].SetActive(j == currentCameraIndex);
                }

                if (cameraComponent.cameras[currentCameraIndex].name == "FreeLook")
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }
}