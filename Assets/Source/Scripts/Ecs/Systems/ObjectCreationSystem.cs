using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    public class ObjectCreationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private ObjectCreationData _objectCreationData = null;
        
        public void Init()
        {
            _objectCreationData = Resources.Load<ObjectCreationData>("Assets/Models/Space Station 2.prefab");
        }

        public void Run()
        {
            if (_objectCreationData == null)
            {
                Debug.LogError("ObjectCreationData not found!");
                return;
            }
            
            // Создание указанного количества объектов.
            for (int j = 0; j < _objectCreationData.numberOfObjectsToCreate; j++)
            {
                // Создаем объект из префаба.
                GameObject newObject = Object.Instantiate(_objectCreationData.objectPrefab);
                // Далее можно производить дополнительную обработку, если нужно.
                // Например, можно добавить какие-то компоненты или установить позицию объекта.
            }
        }
    }
}