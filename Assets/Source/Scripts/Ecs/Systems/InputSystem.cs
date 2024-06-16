using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class InputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTagComponent, DirectionComponent, StabilizationComponent, FuelProductionComponent> inputFilter = null;

        private float _moveX;
        private float _moveY;
        private float _moveZ;

        private float _roll;
        private float _pitch;
        private float _yaw;

        private bool _isStabilizzation = false;
        private bool _isLazerOn;
        private bool _isSwitchingCamera;
        private bool _isSwitchingControl;

        private bool _isFuelProduction;

        public void Run()
        {
            SetDirection();
            SetStabilization();

            foreach (var i in inputFilter)
            {
                ref var playerTagComponent = ref inputFilter.Get1(i);
                if (!playerTagComponent.IsControlledByPlayer) continue;

                ref var directionComponent = ref inputFilter.Get2(i);
                // ref var dockingComponent = ref inputFilter.Get4(i);
                ref var direction = ref directionComponent.Direction;

                direction.x = _moveX;
                direction.y = _moveY;
                direction.z = _moveZ;

                ref var Rroll = ref directionComponent.roll;
                ref var Rpitch = ref directionComponent.pitch;
                ref var Ryaw = ref directionComponent.yaw;

                ref var stabilizationComponent = ref inputFilter.Get3(i);
                ref var IsStabilization = ref stabilizationComponent.isStabization;

                ref var fuelComponent = ref inputFilter.Get4(i);

                _isFuelProduction = fuelComponent.isProductionFuel;

                Rroll = _roll;
                Rpitch = _pitch;
                Ryaw = _yaw;

                IsStabilization = _isStabilizzation;

                _isLazerOn = directionComponent.isLazerOn;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _isLazerOn = !_isLazerOn;
                }
                directionComponent.isLazerOn = _isLazerOn;

                if (Input.GetKeyDown(KeyCode.P))
                {
                    
                    directionComponent.isDocking = true;
                }
                else
                {
                    directionComponent.isDocking = false;
                }

                if (Input.GetKeyDown(KeyCode.G))
                {
                    fuelComponent.isProductionFuel = !_isFuelProduction;
                }
                
                
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    _isSwitchingControl = true;
                }
                else
                {
                    _isSwitchingControl = false;
                }
                directionComponent.isSwitchingControl = _isSwitchingControl;
                // Debug.Log(directionComponent.isSwitchingControl);

                if (Input.GetKeyDown(KeyCode.C))
                {
                    _isSwitchingCamera = true;
                }
                else
                {
                    _isSwitchingCamera = false;
                }
                directionComponent.isSwitchingCamera = _isSwitchingCamera;
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
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                _isStabilizzation = !_isStabilizzation;
            }
        }
    }
}
