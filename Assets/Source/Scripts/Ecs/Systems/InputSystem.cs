using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class InputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTagComponent, DirectionComponent, StabilizationComponent > directionFilter = null;

        private float _moveX;
        private float _moveY;
        private float _moveZ;

        private float _roll;
        private float _pitch;
        private float _yaw;

        private bool _isStabilizzation = false;


        public void Run()
        {
            SetDirection();
            SetStabilization();
            
            foreach (var i in directionFilter)
            {
                ref var directionComponent = ref directionFilter.Get2(i);
                ref var direction = ref directionComponent.Direction;
                
                direction.x = _moveX;
                direction.y = _moveY;
                direction.z = _moveZ;

                ref var Rroll = ref directionComponent.roll;
                ref var Rpitch = ref directionComponent.pitch;
                ref var Ryaw = ref directionComponent.yaw;

                ref var stabilizationComponent = ref directionFilter.Get3(i);
                ref var IsStabilization = ref stabilizationComponent.isStabization;

                Rroll = _roll;
                Rpitch = _pitch;
                Ryaw = _yaw;

                IsStabilization = _isStabilizzation;
            }
        }

        private void SetDirection()
        {
            _moveX = Input.GetAxis("Horizontal");
            _moveY = Input.GetAxis("Vertical");
            _moveZ = Input.GetAxis("AxesZ");

            _roll = Input.GetAxis("Roll");
            _pitch = Input.GetAxis("Pitch");
            _yaw = Input.GetAxis("Yaw");
        }

        private void SetStabilization()
        {
            if (Input.GetKeyDown((KeyCode.BackQuote)))
            {
                _isStabilizzation = !_isStabilizzation;
                Debug.Log($"Stabilization {_isStabilizzation}");
            }
        }
    }
}