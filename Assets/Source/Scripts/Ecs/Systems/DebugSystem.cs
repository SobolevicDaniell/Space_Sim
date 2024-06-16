using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class DebugSystem : IEcsInitSystem
    {
        private readonly EcsFilter<ModelComponent, MovebleComponent, DirectionComponent> debugFilter = null;

        public void Init()
        {
            foreach (var i in debugFilter)
            {
                ref var modelComponent = ref debugFilter.Get1(i);
                ref var movableComponent = ref debugFilter.Get2(i);
                ref var directionComponent = ref debugFilter.Get3(i);

                Debug.Log($"Entity {i} - ModelComponent: {modelComponent.modelTransform != null}, MovableComponent: {movableComponent.rigidbody != null}, DirectionComponent");
            }
        }
    }
}