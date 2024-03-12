using System;
using UnityEngine;

namespace Ecs
{
    public class DockComponent
    {
        public bool triggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                triggered = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Проверяем, столкнулись ли мы с нужным объектом
            // if (other.gameObject.tag == "Player")
            // {
            //     triggered = false;
            // }
            triggered = false;
        }
    }
}