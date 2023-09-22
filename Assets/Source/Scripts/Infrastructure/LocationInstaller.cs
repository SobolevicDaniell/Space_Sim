using Zenject;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform StartPoint;
        public GameObject PlayerPrefab;
        
        public override void InstallBindings()
        {
            BindSouz();
        }

       

        private void BindSouz()
        {
            SouzMuvment souzMuvment = Container
                .InstantiatePrefabForComponent<SouzMuvment>(PlayerPrefab, StartPoint.position, Quaternion.identity, null);

            Container
                .Bind<SouzMuvment>()
                .FromInstance(souzMuvment)
                .AsSingle();
        }
        
        
    }
}