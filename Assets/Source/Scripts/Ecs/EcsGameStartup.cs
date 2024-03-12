using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Ecs
{
    public class EcsGameStartup : MonoBehaviour
    {
        
        private EcsWorld _world;
        private EcsSystems _systems;
        
        [SerializeField]
        private ObjectCreationSystem _objectCreationSystem;


        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();
            
            AddSystems();
        
            _systems.Init();
            _systems.Run();
        }

        private void Update()
        {
            _systems.Run();
        }


        private void AddSystems()
        {
            _systems
                .Add(new InputSystem())
                .Add(new CameraSystem())
                .Add(new MovmentSystem())
                .Add(new StabilizationSystem())
                .Add(new ParticleSrabilizationSystem())
                .Add(new WorldVectorSystem());
        }
        
        

        private void OnDestroy()
        {
            _systems?.Destroy();
            _world?.Destroy();
        }
    }
}

