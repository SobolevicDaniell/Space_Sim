using System;
using UnityEngine;

namespace Ecs
{
    [Serializable]
    public struct StabilizationComponent
    {
        public bool isStabization;

        public GameObject[] forward;
        public GameObject[] back;
        public GameObject[] up;
        public GameObject[] down;
        public GameObject[] left;
        public GameObject[] right;
        
        public GameObject[] rollT;
        public GameObject[] rollF;
        public GameObject[] pitchT;
        public GameObject[] pitchF;
        public GameObject[] yawT;
        public GameObject[] yawF;
    }
}