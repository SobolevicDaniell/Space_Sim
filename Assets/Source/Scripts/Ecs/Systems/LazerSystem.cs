using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class LazerSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<LazerComponent, PlayerTagComponent, DirectionComponent, ResourceComponent> lazerFilter = null;

        public void Run()
        {
            foreach (var i in lazerFilter)
            {
                ref var lazerComponent = ref lazerFilter.Get1(i);
                ref var playerTagComponent = ref lazerFilter.Get2(i);
                ref var directionComponent = ref lazerFilter.Get3(i);
                ref var resourceComponent = ref lazerFilter.Get4(i);

                if (playerTagComponent.IsControlledByPlayer)
                {
                    bool isAsteroidOutOfRange = false;
                    float progress;

                    // Автоматическое выключение лазера при недостатке электричества
                    if (resourceComponent.currentElectricity < resourceComponent.minElectricityLazer)
                    {
                        directionComponent.isLazerOn = false;
                    }

                    if (directionComponent.isLazerOn)
                    {
                        lazerComponent._lineRenderer.enabled = true;
                        Vector3 rayOrigin = lazerComponent._lazerStart.position;
                        Vector3 direction = lazerComponent._lazerStart.forward;

                        RaycastHit hit;
                        Vector3 laserEndPosition = rayOrigin + (direction * lazerComponent._laserRange);

                        Debug.DrawRay(rayOrigin, direction * lazerComponent._laserRange, Color.red, 0.1f);

                        int asteroidLayerMask = LayerMask.GetMask("AsteroidLayer");

                        if (Physics.Raycast(rayOrigin, direction, out hit, lazerComponent._laserRange, asteroidLayerMask, QueryTriggerInteraction.Ignore))
                        {
                            laserEndPosition = hit.point;

                            var hitEntityReference = hit.collider.GetComponent<EcsEntityReference>();
                            if (hitEntityReference != null)
                            {
                                var hitEntity = hitEntityReference.Entity;
                                if (hitEntity != EcsEntity.Null && hitEntity.Has<AsteroidComponent>())
                                {
                                    float distanceToAsteroid = Vector3.Distance(rayOrigin, hit.point);
                                    // Debug.Log($"Distance to Asteroid: {distanceToAsteroid}");

                                    if (distanceToAsteroid <= lazerComponent._maxLazerDistance)
                                    {
                                        lazerComponent.lazerProgress.gameObject.SetActive(true);

                                        ref var asteroidComponent = ref hitEntity.Get<AsteroidComponent>();

                                        asteroidComponent.miningProgress -= Time.deltaTime;

                                        progress = 1 - (asteroidComponent.miningProgress / asteroidComponent.miningProgressTimer);
                                        lazerComponent.lazerProgress.fillAmount = Mathf.Clamp01(progress);

                                        isAsteroidOutOfRange = asteroidComponent.miningProgress <= 0;
                                        if (isAsteroidOutOfRange)
                                        {
                                            resourceComponent.currentMaterials += asteroidComponent.materials;
                                            hitEntity.Destroy();
                                            Object.Destroy(hit.collider.gameObject);
                                        }
                                    }
                                    else
                                    {
                                        isAsteroidOutOfRange = true;
                                        lazerComponent.lazerProgress.gameObject.SetActive(false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Debug.Log("Laser did not hit any asteroid.");
                        }

                        lazerComponent._lineRenderer.SetPosition(0, rayOrigin);
                        lazerComponent._lineRenderer.SetPosition(1, laserEndPosition);
                    }
                    else
                    {
                        lazerComponent._lineRenderer.enabled = false;
                        lazerComponent.lazerProgress.gameObject.SetActive(false);
                    }

                    lazerComponent.errorText.SetActive(isAsteroidOutOfRange);
                }
            }
        }
    }
}
