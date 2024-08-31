using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        private SceneData _sceneData;
        private StaticData _staticData;

        public void Init()
        {
            EcsEntity playerEntity = _world.NewEntity();

            ref var movableComponent = ref playerEntity.Get<MovebleComponent>();
            
            GameObject playerGO = Object.Instantiate(_staticData.playerPrefab, _sceneData.playerSpawnPoint.transform.position, Quaternion.identity);
            movableComponent.rigidbody = playerGO.GetComponent<Rigidbody>();
        }
    }
}