using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class MovmentSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, MovableComponent, DirectionComponent> movableFilter = null;
        
        public void Run()
        {
            foreach (var i in movableFilter)
            { 
                if (!movableFilter.GetEntity(i).Has<ModelComponent>() || 
                    !movableFilter.GetEntity(i).Has<MovableComponent>() || 
                    !movableFilter.GetEntity(i).Has<DirectionComponent>())
                {
                    continue;
                }
                
                ref var modelComponent = ref movableFilter.Get1(i);
                ref var movableComponent = ref movableFilter.Get2(i);
                ref var directionComponent = ref movableFilter.Get3(i);
                

                ref var direction = ref directionComponent.Direction;
                ref var Rroll = ref directionComponent.roll;
                ref var Rpitch = ref directionComponent.pitch;
                ref var Ryaw = ref directionComponent.yaw;
                //ref var IsStabilization = ref directionComponent.isStabization;
                ref var transform = ref modelComponent.modelTransform;
                ref var chatracteRigidbody = ref movableComponent.rigidbody;
                
                ref var speed = ref movableComponent.speed;
                ref var rotationSpeed = ref movableComponent.rotateSpeed;
                
                var rawDirection = (transform.right * direction.x) + (transform.forward * direction.z) + (transform.up * direction.y);
                
                chatracteRigidbody.AddForce(rawDirection * speed);
                
                var torque = new Vector3(Rpitch, Ryaw, -Rroll) * rotationSpeed;
                chatracteRigidbody.AddRelativeTorque(torque, ForceMode.Force);
                
            }
        }
    }
}