using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class ParticleSrabilizationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<StabilizationComponent, DirectionComponent, ResourceComponent> paricleFilter = null;

        public void Run()
        {
            foreach (var i in paricleFilter)
            {
                ref var stabilizationComponent = ref paricleFilter.Get1(i);
                ref var directionComponent = ref paricleFilter.Get2(i);
                ref var resourceComponent = ref paricleFilter.Get3(i);

                if (!directionComponent.isStabization)
                {
                    if (directionComponent.Direction.x > 0)
                    {
                        if (stabilizationComponent.left != null)
                        {
                            foreach (var j in stabilizationComponent.left)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }

                        if (stabilizationComponent.right != null)
                        {
                            foreach (var j in stabilizationComponent.right)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }
                    }
                    else if (directionComponent.Direction.x < 0)
                    {
                        if (stabilizationComponent.left != null)
                        {
                            foreach (var j in stabilizationComponent.left)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.right != null)
                        {
                            foreach (var j in stabilizationComponent.right)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }
                    }

                    if (directionComponent.Direction.x == 0 && directionComponent.roll == 0 &&
                        directionComponent.yaw == 0)
                    {
                        if (stabilizationComponent.left != null)
                        {
                            foreach (var j in stabilizationComponent.left)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.right != null)
                        {
                            foreach (var j in stabilizationComponent.right)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }
                    }

                    if (directionComponent.Direction.y > 0)
                    {
                        if (stabilizationComponent.down != null)
                        {
                            foreach (var j in stabilizationComponent.down)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }

                        if (stabilizationComponent.up != null)
                        {
                            foreach (var j in stabilizationComponent.up)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }
                    }
                    else if (directionComponent.Direction.y < 0)
                    {
                        if (stabilizationComponent.down != null)
                        {
                            foreach (var j in stabilizationComponent.down)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.up != null)
                        {
                            foreach (var j in stabilizationComponent.up)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }
                    }

                    if (directionComponent.Direction.y == 0 && directionComponent.pitch == 0)
                    {
                        if (stabilizationComponent.down != null)
                        {
                            foreach (var j in stabilizationComponent.down)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.up != null)
                        {
                            foreach (var j in stabilizationComponent.up)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }
                    }

                    if (directionComponent.Direction.z > 0)
                    {
                        if (stabilizationComponent.back != null)
                        {
                            foreach (var j in stabilizationComponent.back)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }

                        if (stabilizationComponent.forward != null)
                        {
                            foreach (var k in stabilizationComponent.forward)
                            {
                                k?.gameObject.SetActive(false);
                            }
                        }
                    }
                    else if (directionComponent.Direction.z < 0)
                    {
                        if (stabilizationComponent.back != null)
                        {
                            foreach (var j in stabilizationComponent.back)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.forward != null)
                        {
                            foreach (var k in stabilizationComponent.forward)
                            {
                                k?.gameObject.SetActive(true);
                            }
                        }
                    }

                    if (directionComponent.Direction.z == 0 && directionComponent.yaw == 0)
                    {
                        if (stabilizationComponent.forward != null)
                        {
                            foreach (var k in stabilizationComponent.forward)
                            {
                                k?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.back != null)
                        {
                            foreach (var j in stabilizationComponent.back)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }
                    }

                    if (directionComponent.roll > 0)
                    {
                        if (stabilizationComponent.rollT != null)
                        {
                            foreach (var j in stabilizationComponent.rollT)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }

                        if (stabilizationComponent.rollF != null)
                        {
                            foreach (var j in stabilizationComponent.rollF)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }
                    }
                    else if (directionComponent.roll < 0)
                    {
                        if (stabilizationComponent.rollT != null)
                        {
                            foreach (var j in stabilizationComponent.rollT)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.rollF != null)
                        {
                            foreach (var j in stabilizationComponent.rollF)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }
                    }

                    if (directionComponent.pitch < 0)
                    {
                        if (stabilizationComponent.pitchT != null)
                        {
                            foreach (var j in stabilizationComponent.pitchT)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }

                        if (stabilizationComponent.pitchF != null)
                        {
                            foreach (var j in stabilizationComponent.pitchF)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }
                    }
                    else if (directionComponent.pitch > 0)
                    {
                        if (stabilizationComponent.pitchT != null)
                        {
                            foreach (var j in stabilizationComponent.pitchT)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.pitchF != null)
                        {
                            foreach (var j in stabilizationComponent.pitchF)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }
                    }

                    if (directionComponent.yaw > 0)
                    {
                        if (stabilizationComponent.yawT != null)
                        {
                            foreach (var j in stabilizationComponent.yawT)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }

                        if (stabilizationComponent.yawF != null)
                        {
                            foreach (var j in stabilizationComponent.yawF)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }
                    }
                    else if (directionComponent.yaw < 0)
                    {
                        if (stabilizationComponent.yawT != null)
                        {
                            foreach (var j in stabilizationComponent.yawT)
                            {
                                j?.gameObject.SetActive(false);
                            }
                        }

                        if (stabilizationComponent.yawF != null)
                        {
                            foreach (var j in stabilizationComponent.yawF)
                            {
                                j?.gameObject.SetActive(true);
                            }
                        }
                    }
                }
                else
                {
                    if (stabilizationComponent.right != null)
                    {
                        foreach (var j in stabilizationComponent.right)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.left != null)
                    {
                        foreach (var j in stabilizationComponent.left)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.up != null)
                    {
                        foreach (var j in stabilizationComponent.up)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.down != null)
                    {
                        foreach (var j in stabilizationComponent.down)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.forward != null)
                    {
                        foreach (var j in stabilizationComponent.forward)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.back != null)
                    {
                        foreach (var j in stabilizationComponent.back)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.rollT != null)
                    {
                        foreach (var j in stabilizationComponent.rollT)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.rollF != null)
                    {
                        foreach (var j in stabilizationComponent.rollF)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.pitchT != null)
                    {
                        foreach (var j in stabilizationComponent.pitchT)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.pitchF != null)
                    {
                        foreach (var j in stabilizationComponent.pitchF)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.yawT != null)
                    {
                        foreach (var j in stabilizationComponent.yawT)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                    if (stabilizationComponent.yawF != null)
                    {
                        foreach (var j in stabilizationComponent.yawF)
                        {
                            j?.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
