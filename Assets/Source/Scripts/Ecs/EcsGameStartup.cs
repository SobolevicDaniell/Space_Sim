using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Ecs
{
    public class EcsGameStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Awake()
        {
            // Debug.Log("Awake: Initializing EcsWorld");
            _world = new EcsWorld();
        }

        private void Start()
        {
            // Debug.Log("Start: Initializing EcsSystems");
            _systems = new EcsSystems(_world);
            _systems.ConvertScene();
            AddSystems();
            _systems.Init();
        }

        private void Update()
        {
            if (_systems != null)
            {
                // Debug.Log("Run");
                _systems.Run();
            }
        }

        private void AddSystems()
        {
            // Debug.Log("Adding Systems");
            _systems
                .Add(new InputSystem())
                .Add(new CameraSystem())
                .Add(new MovementSystem())
                .Add(new StabilizationSystem())
                .Add(new ParticleSrabilizationSystem())
                .Add(new ControlSwitchSystem())
                .Add(new ResourceSpendingSystem())
                // .Add(new TransferSystem())
                .Add(new UIObjectSystem())
                .Add(new LazerSystem())
                .Add(new AsteroidSpawnSystem())
                .Add(new RestartSystem())
                .Add(new DockingSystem())
                .Add(new FuelProductionSystem())
                ;
        }

        public EcsWorld GetWorld()
        {
            return _world;
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                // Debug.Log("Destroying Systems");
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                // Debug.Log("Destroying World");
                _world.Destroy();
                _world = null;
            }
        }

        public EcsSystems GetSystems()
        {
            return _systems;
        }
    }
}
