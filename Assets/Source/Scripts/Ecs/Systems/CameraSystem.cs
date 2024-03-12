using System;
using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Ecs
{
    sealed class CameraSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<CameraComponent, PlayerTagComponent > cameraFilter = null;

        private readonly KeyCode[] cameraSwitchKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 ,KeyCode.Alpha4};

        
        
        public void Run()
        {
            // Перебираем клавиши для переключения камер
            for (int i = 0; i < cameraSwitchKeys.Length; i++)
            {
                // Если нажата соответствующая клавиша, переключаем камеру
                if (Input.GetKeyDown(cameraSwitchKeys[i]))
                {
                    SwitchCamera(i);
                }
            }
        }

        private void SwitchCamera(int cameraIndex)
        {
            // Debug.Log(cameraIndex);

            foreach (var i in cameraFilter)
            {
                ref var cameraComponent = ref cameraFilter.Get1(i);
                foreach (var camera in cameraComponent.cameras)
                {
                    camera.gameObject.SetActive(false);
                }

                cameraComponent.cameras[cameraIndex].SetActive(true);
                
            }
        }
    }
}