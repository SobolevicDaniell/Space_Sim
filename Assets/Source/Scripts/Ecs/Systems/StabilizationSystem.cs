using Leopotam.Ecs;

namespace Ecs
{
    sealed class StabilizationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovebleComponent, StabilizationComponent> stabilizationFilter = null;
        
        public void Run()
        {
            foreach (var i in stabilizationFilter)
            {
                ref var movableComponent = ref stabilizationFilter.Get1(i);
                ref var stabilizationComponent = ref stabilizationFilter.Get2(i);

                ref var chatracteRigidbody = ref movableComponent.rigidbody;
                ref var stabilizationObjectForward = ref stabilizationComponent.forward;
                
                ref var IsStabilization = ref stabilizationComponent.isStabization;
                
                if (IsStabilization)
                {
                    // chatracteRigidbody.drag = 0.8f;
                    chatracteRigidbody.angularDrag = 0.8f;
                    // foreach (var obGameObject in stabilizationObjectForward)
                    // {
                    //     obGameObject.gameObject.SetActive(true);
                    // }
                }
                else
                {
                    // chatracteRigidbody.drag = 0f;
                    chatracteRigidbody.angularDrag = 0f;
                    // foreach (var obGameObject in stabilizationObjectForward)
                    // {
                    //     obGameObject.gameObject.SetActive(false);
                    // }
                }
            }
        }
    }
}