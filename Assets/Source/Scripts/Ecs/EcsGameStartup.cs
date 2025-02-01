using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using UnityEngine.InputSystem;

namespace Ecs
{
    public class EcsGameStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
<<<<<<< Updated upstream
=======

        [SerializeField] private SceneData _sceneData;
        [SerializeField] private InputActionAsset _inputActions; // Передаем InputActionAsset через инспектор
>>>>>>> Stashed changes

        private void Awake()
        {
            _world = new EcsWorld();
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
            _systems?.Run();
        }

        private void AddSystems()
        {
            var inputSystem = new InputSystem(_inputActions);
            inputSystem.Init(); // Вызов инициализации перед добавлением в ECS
            
            _systems
<<<<<<< Updated upstream
                .Add(new InputSystem())
                .Add(new CameraSystem())
=======
                .Add(inputSystem)
                .Add(new CameraSystem(_sceneData))
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                .Add(new FuelProductionSystem())
                ;
        }

        public EcsWorld GetWorld()
        {
            return _world;
=======
                .Add(new FuelProductionSystem());
>>>>>>> Stashed changes
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

        public EcsWorld GetWorld() => _world;
        public EcsSystems GetSystems() => _systems;
    }
}
