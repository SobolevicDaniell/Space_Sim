using System.Numerics;
using Leopotam.Ecs;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Ecs
{
    sealed class WorldVectorSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter< WorldVector, MovableComponent> _filter = null;

        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var worldVector = ref _filter.Get1(i);
                ref var movableComponent = ref _filter.Get2(i);

                Vector3 direction = /*worldVector.worldPosition - */movableComponent.rigidbody.transform.position;

                worldVector.selfWorldVector = direction;
                Debug.Log(worldVector.selfWorldVector);
            }
        }
    }
}