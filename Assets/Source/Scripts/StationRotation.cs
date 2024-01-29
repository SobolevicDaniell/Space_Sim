using UnityEngine;

namespace Source.Scripts
{
    public class StationAntetaRotation : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject gameObject;
    
  

        void Update()
        {
            //gameObject.transform.Rotate(0,speed* Time.deltaTime,0);
            //gameObject.transform.Rotate(0,0,speed* Time.deltaTime);
        }
    }
}
