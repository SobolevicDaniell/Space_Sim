using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct DirectionComponent
    {
        public Vector3 Direction;
        public float roll;
        public float pitch;
        public float yaw;

        public bool isStabization;
        public bool isLazerOn;
        public bool isDocking;
        
        
        public bool isSwitchingCamera;
        public bool isSwitchingControl;
    }
}