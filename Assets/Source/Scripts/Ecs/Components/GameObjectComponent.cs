using UnityEngine;

namespace Ecs
{
    public struct GameObjectComponent
    {
        public GameObject Value;

        public void Reset()
        {
            Value = null;
        }
    }
}