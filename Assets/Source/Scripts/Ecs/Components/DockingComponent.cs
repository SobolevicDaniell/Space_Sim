using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct DockingComponent
    {
        public bool isDocked;
        public bool readyToInteract;
        public GameObject gameObjectWithDock;
        
        public GameObject transferMenu;
    }
}