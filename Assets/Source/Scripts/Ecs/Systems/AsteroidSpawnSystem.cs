using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class AsteroidSpawnSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<SpawnAsteroidComponent> _spawnFilter = null;

        public void Init()
        {
            foreach (var i in _spawnFilter)
            {
                ref var spawnComponent = ref _spawnFilter.Get1(i);

                for (int j = 0; j < spawnComponent.asteroidsCount; j++)
                {
                    Vector3 spawnPosition = GetRandomPositionInZone(spawnComponent.zoneToSpawn);

                    GameObject asteroidPrefab = spawnComponent.asteroidPrefabs[Random.Range(0, spawnComponent.asteroidPrefabs.Count)];
                    GameObject asteroidInstance = Object.Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

                    var asteroidEntity = _world.NewEntity();
                    ref var asteroidComponent = ref asteroidEntity.Get<AsteroidComponent>();
                    asteroidComponent.materials = Random.Range(spawnComponent.minMaterials, spawnComponent.maxMaterials);
                    asteroidComponent.miningProgress = Random.Range(spawnComponent.minMiningProgress, spawnComponent.maxMiningProgress);
                    asteroidComponent.miningProgressTimer = asteroidComponent.miningProgress;

                    // Убедитесь, что EcsEntityReference добавляется
                    var entityReference = asteroidInstance.GetComponent<EcsEntityReference>();
                    if (entityReference == null)
                    {
                        entityReference = asteroidInstance.AddComponent<EcsEntityReference>();
                    }
                    entityReference.Entity = asteroidEntity;

                    // Debug.Log($"Asteroid {j} material: {asteroidComponent.materials}");
                }
            }
        }

        private Vector3 GetRandomPositionInZone(GameObject zone)
        {
            Bounds bounds = zone.GetComponent<Collider>().bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}
