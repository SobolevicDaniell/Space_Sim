using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct WorldVector
    {
        public Vector3 selfWorldVector;
        public Vector3 worldPosition;
    }
}