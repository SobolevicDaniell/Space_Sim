using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class RestartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RestartComponent> _restartFilter = null;

        public void Run()
        {
            foreach (var i in _restartFilter)
            {
                ref var restart = ref _restartFilter.Get1(i);

                if (restart.isRestart)
                {
                    restart.restartText.SetActive(true);
                }
            }
        }
    }
}