using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Source.Scripts.Muvment
{
    public class StationMuvment : MonoBehaviour , IMuvment
    {
        public GameObject docingObject;
        
        [Inject]
        private void Construct(SouzMuvment souzMuvment)
        {
            docingObject = souzMuvment.gameObject;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == docingObject)
            {
                Debug.Log("Collision with station!");
            }
        }
    }
}