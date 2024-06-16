using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, MovebleComponent, DirectionComponent, RestartComponent> movementFilter = null;

        public void Run()
        {
            foreach (var i in movementFilter)
            {
                // Логирование для проверки наличия компонентов
                // Debug.Log($"Processing Entity {i}");

                if (!movementFilter.GetEntity(i).Has<ModelComponent>() || 
                    !movementFilter.GetEntity(i).Has<MovebleComponent>() || 
                    !movementFilter.GetEntity(i).Has<DirectionComponent>())
                {
                    // Debug.Log($"Entity {i} is missing one or more components.");
                    continue;
                }
                
                ref var modelComponent = ref movementFilter.Get1(i);
                ref var movableComponent = ref movementFilter.Get2(i);
                ref var directionComponent = ref movementFilter.Get3(i);
                ref var restartComponent = ref movementFilter.Get4(i);

                if (!restartComponent.isRestart)
                {
                    ref var direction = ref directionComponent.Direction;
                    ref var Rroll = ref directionComponent.roll;
                    ref var Rpitch = ref directionComponent.pitch;
                    ref var Ryaw = ref directionComponent.yaw;

                    ref var transform = ref modelComponent.modelTransform;
                    ref var chatracteRigidbody = ref movableComponent.rigidbody;

                    ref var speed = ref movableComponent.speed;
                    ref var rotationSpeed = ref movableComponent.rotateSpeed;
                    ref var rotationRollSpeed = ref movableComponent.rotateRollSpeed;

                    var rawDirection = (transform.right * direction.x) + (transform.forward * direction.z) + (transform.up * direction.y);

                    // Debug.Log($"Entity {i} - Applying Force: {rawDirection * speed}");

                    chatracteRigidbody.AddForce(rawDirection * speed);

                    var torque = new Vector3(Rpitch, Ryaw, -Rroll * rotationRollSpeed) * rotationSpeed;
                    // Debug.Log($"Entity {i} - Applying Torque: {torque}");

                    chatracteRigidbody.AddRelativeTorque(torque, ForceMode.Force);
                }
                    
            }
        }
    }
}