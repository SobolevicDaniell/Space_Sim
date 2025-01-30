using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Ecs
{
    public class EcsGameStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private InputSystem _inputSystem;
        [SerializeField] private SceneData _scaneData;


        private void Awake()
        {
            _world = new EcsWorld();
            // _inputSystem = new InputSystem();
            // _scaneData = new SceneData();
        }

        private void Start()
        {
            _systems = new EcsSystems(_world);
            _systems.ConvertScene();
            AddSystems();
            _systems.Init();
        }

        private void Update()
        {
            if (_systems != null)
            {
                _systems.Run();
            }
        }

        private void AddSystems()
        {
            _systems
                .Add(new InputSystem())
                .Add(new CameraSystem(_scaneData))
                .Add(new MovementSystem())
                .Add(new StabilizationSystem())
                .Add(new ParticleSrabilizationSystem())
                .Add(new ControlSwitchSystem())
                .Add(new ResourceSpendingSystem())
                .Add(new UIObjectSystem())
                .Add(new LazerSystem())
                .Add(new AsteroidSpawnSystem())
                .Add(new RestartSystem())
                .Add(new DockingSystem())
                .Add(new FuelProductionSystem())
                // .Add(new TestEventSystem())
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
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
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
