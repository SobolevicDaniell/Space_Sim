using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct MovebleComponent
    {
        public Rigidbody rigidbody;
        public float speed;
        public float rotateSpeed;
        public float rotateRollSpeed;
    }
}