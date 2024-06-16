using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ecs
{
    [Serializable]
    public struct LazerComponent
    {
        public LineRenderer _lineRenderer;
        public Transform _lazerStart;
        public float _laserRange;
        public float _maxLazerDistance;

        public GameObject errorText;
        public Image lazerProgress;

    }
}