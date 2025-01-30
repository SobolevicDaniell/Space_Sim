using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class InputSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerTagComponent, DirectionComponent, StabilizationComponent, FuelProductionComponent> inputFilter = null;

        private const float DEAD_ZONE = 0.4f; // Значение мёртвой зоны

        private Vector2 _moveX;
        private Vector2 _moveY;
        private float _moveZ;

        private float _roll;
        private Vector2 _pitch;
        private Vector2 _yaw;

        private bool _isStabilizzation = false;
        private bool _isLazerOn;
        private bool _isSwitchingCamera;
        private bool _isSwitchingControl;

        private bool _isFuelProduction;

        public event Action SwitchControl;
        public event Action SwitchCamera;
        
        public void Run()
        {
            SetDirection();
            SetStabilization();

            foreach (var i in inputFilter)
            {
                ref var playerTagComponent = ref inputFilter.Get1(i);
                if (!playerTagComponent.IsControlledByPlayer) continue;

                ref var directionComponent = ref inputFilter.Get2(i);
                ref var direction = ref directionComponent.Direction;

                direction.x = _moveX.x;
                direction.y = _moveY.y;
                direction.z = _moveZ;

                ref var Rroll = ref directionComponent.roll;
                ref var Rpitch = ref directionComponent.pitch;
                ref var Ryaw = ref directionComponent.yaw;

                ref var stabilizationComponent = ref inputFilter.Get3(i);
                ref var IsStabilization = ref stabilizationComponent.isStabization;

                ref var fuelComponent = ref inputFilter.Get4(i);

                _isFuelProduction = fuelComponent.isProductionFuel;

                Rroll = _roll;
                Rpitch = _pitch.y;
                Ryaw = _yaw.x;

                IsStabilization = _isStabilizzation;

                _isLazerOn = directionComponent.isLazerOn;
                if (OVRInput.GetDown(OVRInput.Button.Two)) // Кнопка A
                {
                    _isLazerOn = !_isLazerOn;
                }
                directionComponent.isLazerOn = _isLazerOn;

                directionComponent.isDocking = OVRInput.GetDown(OVRInput.Button.Three); // Кнопка B

                // if (OVRInput.GetDown(OVRInput.Button.Four)) // Кнопка X
                // {
                //     fuelComponent.isProductionFuel = !_isFuelProduction;
                // }

                // directionComponent.isSwitchingControl = OVRInput.GetDown(OVRInput.Button.Start);
                if (OVRInput.GetDown(OVRInput.Button.Four) || Input.GetKeyDown(KeyCode.F))
                {
                    // SwitchControl?.Invoke();
                    var entity =_world.NewEntity();
                    entity.Get<EventControlSwitch>().eventControlSwitch = true;
                }
                if (OVRInput.GetDown(OVRInput.Button.Three) || Input.GetKeyDown(KeyCode.R))
                {
                    Debug.Log("SwitchCamera вызван");
                    // SwitchCamera?.Invoke();

                    var entity =_world.NewEntity();
                    entity.Get<EventCameraSwitch>().eventCameraSwitch = true;
                }
                // directionComponent.isSwitchingCamera = OVRInput.GetDown(OVRInput.Button.Three); // Кнопка Y
                // if (Input.GetKeyUp(KeyCode.E))
                // {
                //     Debug.Log("Создаем событие TestEvent!");

                //     var entity = _world.NewEntity();
                //     entity.Get<TestEventComponent>().testEvent = true;
                // }
            }
        }

        private void SetDirection()
        {
            _moveX = ApplyDeadZone(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick)); // Левый стик
            _moveY = ApplyDeadZone(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick)); // Левый стик
            _moveZ = ApplyDeadZone(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) - OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)); // Триггеры

            // Управление ориентацией
            _roll = ApplyDeadZone(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) - OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger));
            _pitch = ApplyDeadZone(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick)); // Правый стик
            _yaw = ApplyDeadZone(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick)); // Правый стик
        }

        private void SetStabilization()
        {
            if (OVRInput.GetDown(OVRInput.Button.One)) // Кнопка A
            {
                _isStabilizzation = !_isStabilizzation;
            }
        }

        private float ApplyDeadZone(float value)
        {
            return Mathf.Abs(value) > DEAD_ZONE ? value : 0f;
        }

        private Vector2 ApplyDeadZone(Vector2 value)
        {
            return new Vector2(
                Mathf.Abs(value.x) > DEAD_ZONE ? value.x : 0f,
                Mathf.Abs(value.y) > DEAD_ZONE ? value.y : 0f
            );
        }
    }
}
