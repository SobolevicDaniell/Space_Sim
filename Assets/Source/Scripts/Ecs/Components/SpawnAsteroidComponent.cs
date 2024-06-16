using System.Collections.Generic;
using UnityEngine;

namespace Ecs
{
    [System.Serializable]
    public struct SpawnAsteroidComponent
    {
        public List<GameObject> asteroidPrefabs;
        public int asteroidsCount;
        public GameObject zoneToSpawn;
        public int minMaterials;
        public int maxMaterials;
        public float minMiningProgress;
        public float maxMiningProgress;
    }
}