using Leopotam.Ecs;
using UnityEngine;

namespace Ecs
{
    sealed class ParticleSrabilizationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<StabilizationComponent , DirectionComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var stabilizationComponent = ref _filter.Get1(i);
                ref var directionComponent = ref _filter.Get2(i);

                // Debug.Log(directionComponent.Direction);
                
                if (!directionComponent.isStabization)
                {
                    
                    
                    if (directionComponent.Direction.x > 0)
                    {
                        foreach (var j in stabilizationComponent.left)
                        {
                            j.gameObject.SetActive(true);
                        }
                        foreach (var j in stabilizationComponent.right)
                        {
                            j.gameObject.SetActive(false);
                        }
                    }else if (directionComponent.Direction.x < 0)
                    {
                        foreach (var j in stabilizationComponent.left)
                        {
                            j.gameObject.SetActive(false);
                        }
                        foreach (var j in stabilizationComponent.right)
                        {
                            j.gameObject.SetActive(true);
                        }
                    }
                    if (directionComponent.Direction.x == 0 && directionComponent.roll == 0 && directionComponent.yaw == 0)
                    {
                        foreach (var j in stabilizationComponent.left)
                        {
                            j.gameObject.SetActive(false);
                        }
                        foreach (var j in stabilizationComponent.right)
                        {
                            j.gameObject.SetActive(false);
                        }
                    }
                    
                    
                    if (directionComponent.Direction.y > 0)
                    {
                        foreach (var j in stabilizationComponent.down)
                        {
                            j.gameObject.SetActive(true);
                        }
                        foreach (var j in stabilizationComponent.up)
                        {
                            j.gameObject.SetActive(false);
                        }
                    }else if (directionComponent.Direction.y < 0)
                    {
                        foreach (var j in stabilizationComponent.down)
                        {
                            j.gameObject.SetActive(false);
                        }
                        foreach (var j in stabilizationComponent.up)
                        {
                            j.gameObject.SetActive(true);
                        }
                    }
                    if (directionComponent.Direction.y == 0 && directionComponent.pitch == 0)
                    {
                        foreach (var j in stabilizationComponent.down)
                        {
                            j.gameObject.SetActive(false);
                        }
                        foreach (var j in stabilizationComponent.up)
                        {
                            j.gameObject.SetActive(false);
                        }
                    }
                    
                    
                    if (directionComponent.Direction.z > 0)
                    {
                        foreach (var j in stabilizationComponent.back)
                        {
                            j.gameObject.SetActive(true);
                        }
                        foreach (var k in stabilizationComponent.forward)
                        {
                            k.gameObject.SetActive(false);
                        }
                        
                    }else if (directionComponent.Direction.z < 0)
                    {
                        foreach (var j in stabilizationComponent.back)
                        {
                            j.gameObject.SetActive(false);
                        }
                        foreach (var k in stabilizationComponent.forward)
                        {
                            k.gameObject.SetActive(true);
                        }
                    }
                    if (directionComponent.Direction.z == 0 && directionComponent.yaw == 0)
                    {
                        foreach (var k in stabilizationComponent.forward)
                        {
                            k.gameObject.SetActive(false);
                        }
                        foreach (var j in stabilizationComponent.back)
                        {
                            j.gameObject.SetActive(false);
                        }
                    }



                    if (directionComponent.roll > 0)
                    {
                        foreach (var j in stabilizationComponent.rollT)
                        {
                            j.gameObject.SetActive(true);
                        }
                        foreach (var j in stabilizationComponent.rollF)
                        {
                            j.gameObject.SetActive(false);
                        }
                    }else if (directionComponent.roll < 0)
                    {
                        foreach (var j in stabilizationComponent.rollT)
                        {
                            j.gameObject.SetActive(false);
                        }
                        foreach (var j in stabilizationComponent.rollF)
                        {
                            j.gameObject.SetActive(true);
                        }
                    }
                    
                    
                    
                    if (directionComponent.pitch > 0)
                    {
                        foreach (var j in stabilizationComponent.pitchT)
                        {
                            j.gameObject.SetActive(true);
                        }
                        foreach (var j in stabilizationComponent.pitchF)
                        {
                            j.gameObject.SetActive(false);
                        }
                    }else if (directionComponent.pitch < 0)
                    {
                        foreach (var j in stabilizationComponent.pitchT)
                        {
                            j.gameObject.SetActive(false);
                        }
                        foreach (var j in stabilizationComponent.pitchF)
                        {
                            j.gameObject.SetActive(true);
                        }
                    }
                    
                    
                    
                    if (directionComponent.yaw > 0)
                    {
                        foreach (var j in stabilizationComponent.yawT)
                        {
                            j.gameObject.SetActive(true);
                        }
                        foreach (var j in stabilizationComponent.yawF)
                        {
                            j.gameObject.SetActive(false);
                        }
                    }else if (directionComponent.yaw < 0)
                    {
                        foreach (var j in stabilizationComponent.yawT)
                        {
                            j.gameObject.SetActive(false);
                        }
                        foreach (var j in stabilizationComponent.yawF)
                        {
                            j.gameObject.SetActive(true);
                        }
                    }
                    
                    
                    
                }
                else
                {
                    if (directionComponent.Direction.x > 0)
                    {
                        foreach (var j in stabilizationComponent.forward)
                        {
                            
                        }
                    }
                }
            }
        }
    }
}