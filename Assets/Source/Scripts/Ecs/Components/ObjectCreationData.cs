using UnityEngine;

namespace Ecs
{
    [CreateAssetMenu(fileName = "ObjectCreationData", menuName = "ECS/ObjectCreationData", order = 1)]
    public class ObjectCreationData : ScriptableObject
    {
        public GameObject objectPrefab;
        public int numberOfObjectsToCreate = 5;
    }
}