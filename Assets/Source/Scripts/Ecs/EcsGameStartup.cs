using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Source.Scripts.Ecs
{
    
    class EcsGameStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        void Init()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems.ConvertScene();
                
            AddInjections();
            AddSystems();
            AddOneFrames();
            
            _systems.Init();
        }

        private void AddInjections()
        {
            
        }

        private void AddSystems()
        {
            
        }

        private void AddOneFrames()
        {
            
        }
        
        private void Update()
        {
            _systems?.Run();
        }

        void Destroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}
