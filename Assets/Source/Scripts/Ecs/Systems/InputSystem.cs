using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ecs
{
    sealed class InputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTagComponent, DirectionComponent, StabilizationComponent, FuelProductionComponent> inputFilter = null;
<<<<<<< Updated upstream

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
=======
        
        private const float DEAD_ZONE = 0.4f;

        private Vector2 _move;
        private Vector2 _look;
        private float _thrust;
        private float _roll;
        private bool _isStabilization;
        private bool _isLazerOn;

        private InputActionAsset _inputActions;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _thrustAction;
        private InputAction _rollAction;
        private InputAction _stabilizationAction;
        private InputAction _fireAction;
        private InputAction _switchControlAction;
        private InputAction _switchCameraAction;

        public InputSystem(InputActionAsset inputActions)
        {
            _inputActions = inputActions;
        }

        public void Init()
        {
            if (_inputActions == null)
            {
                Debug.LogError("InputActionAsset не задан!");
                return;
            }

            var playerMap = _inputActions.FindActionMap("Player");
            if (playerMap == null)
            {
                Debug.LogError("Не найден ActionMap 'Player'!");
                return;
            }

            _moveAction = playerMap.FindAction("Move");
            _lookAction = playerMap.FindAction("Look");
            _thrustAction = playerMap.FindAction("Thrust");
            _rollAction = playerMap.FindAction("Roll");
            _stabilizationAction = playerMap.FindAction("Stabilization");
            _fireAction = playerMap.FindAction("Fire");
            _switchControlAction = playerMap.FindAction("SwitchControl");
            _switchCameraAction = playerMap.FindAction("SwitchCamera");

            if (_moveAction == null || _lookAction == null || _thrustAction == null)
            {
                Debug.LogError("Один из InputAction не найден!");
            }
        }
>>>>>>> Stashed changes

        public void Run()
        {
            ProcessInput();
            
            foreach (var i in inputFilter)
            {
                ref var playerTagComponent = ref inputFilter.Get1(i);
                if (!playerTagComponent.IsControlledByPlayer) continue;

                ref var directionComponent = ref inputFilter.Get2(i);
<<<<<<< Updated upstream
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

                if (Input.GetKeyDown(KeyCode.C))
                {
                    _isSwitchingCamera = true;
                }
                else
                {
                    _isSwitchingCamera = false;
                }
                directionComponent.isSwitchingCamera = _isSwitchingCamera;
=======
                directionComponent.Direction = new Vector3(_move.x, _move.y, _thrust);
                directionComponent.roll = _roll;
                directionComponent.pitch = _look.y;
                directionComponent.yaw = _look.x;
                directionComponent.isLazerOn = _isLazerOn;

                ref var stabilizationComponent = ref inputFilter.Get3(i);
                stabilizationComponent.isStabization = _isStabilization;
>>>>>>> Stashed changes
            }
        }

        private void ProcessInput()
        {
<<<<<<< Updated upstream
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
=======
            _move = ApplyDeadZone(_moveAction?.ReadValue<Vector2>() ?? Vector2.zero);
            _look = ApplyDeadZone(_lookAction?.ReadValue<Vector2>() ?? Vector2.zero);
            _thrust = ApplyDeadZone(_thrustAction?.ReadValue<float>() ?? 0f);
            _roll = ApplyDeadZone(_rollAction?.ReadValue<float>() ?? 0f);

            if (_stabilizationAction?.triggered == true)
                _isStabilization = !_isStabilization;
            
            if (_fireAction?.triggered == true)
                _isLazerOn = !_isLazerOn;
            
            if (_switchControlAction?.triggered == true)
>>>>>>> Stashed changes
            {
                var entity = _world.NewEntity();
                entity.Get<EventControlSwitch>().eventControlSwitch = true;
            }

            if (_switchCameraAction?.triggered == true)
            {
                var entity = _world.NewEntity();
                entity.Get<EventCameraSwitch>().eventCameraSwitch = true;
            }
        }
<<<<<<< Updated upstream
=======

        private float ApplyDeadZone(float value) => Mathf.Abs(value) > DEAD_ZONE ? value : 0f;
        private Vector2 ApplyDeadZone(Vector2 value) => new Vector2(
            Mathf.Abs(value.x) > DEAD_ZONE ? value.x : 0f,
            Mathf.Abs(value.y) > DEAD_ZONE ? value.y : 0f
        );
>>>>>>> Stashed changes
    }
}
