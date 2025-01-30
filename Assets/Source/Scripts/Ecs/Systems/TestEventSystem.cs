using Ecs;
using Leopotam.Ecs;
using UnityEngine;

sealed class TestEventSystem : IEcsRunSystem
{
    private readonly EcsFilter<TestEventComponent> eventFilter = null;

    public void Run()
    {
        foreach (var i in eventFilter)
        {
            ref var testEvent = ref eventFilter.Get1(i);

            if (testEvent.testEvent)
            {
                Debug.Log("Событие TestEvent произошло!");

                // Удаляем компонент после обработки
                ref var entity = ref eventFilter.GetEntity(i);
                entity.Del<TestEventComponent>();
            }
        }
    }
}
