using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct CameraComponent
    {
        public GameObject[] camerasPosition;
        public int currentCameraIndex;
        public bool isFirstTime;
    }
}