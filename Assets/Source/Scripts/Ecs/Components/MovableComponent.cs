using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct MovableComponent
    {
        public Rigidbody rigidbody;
        public float speed;
        public float rotateSpeed;
    }
}