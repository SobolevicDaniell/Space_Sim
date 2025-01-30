using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class ControlSwitchSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerTagComponent> controlFilter = null;
        private readonly EcsFilter<EventControlSwitch> eventFilter = null;
        private int _currentPlayerIndex = 0;

        public void Run()
        {
            foreach (var eventIndex in eventFilter)
            {
                if (controlFilter.GetEntitiesCount() > 0)
                {
                    ref var currentPlayerTagComponent = ref controlFilter.Get1(_currentPlayerIndex);
                    currentPlayerTagComponent.IsControlledByPlayer = false;

                    _currentPlayerIndex = (_currentPlayerIndex + 1) % controlFilter.GetEntitiesCount();

                    ref var nextPlayerTagComponent = ref controlFilter.Get1(_currentPlayerIndex);
                    nextPlayerTagComponent.IsControlledByPlayer = true;

                    Debug.Log($"Управление переключено на сущность с индексом {_currentPlayerIndex}");

                    var entity = _world.NewEntity();
                    ref var cameraOnEvent = ref entity.Get<EventCameraOn>();
                    cameraOnEvent.EntityId = _currentPlayerIndex; // Устанавливаем, если нужно
                }

                ref var eventEntity = ref eventFilter.GetEntity(eventIndex);
                eventEntity.Del<EventControlSwitch>();
            }
        }
    }
}
