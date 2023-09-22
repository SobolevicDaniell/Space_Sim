using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationRotation : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject gameObject;
    
  

    void Update()
    {
        gameObject.transform.Rotate(0,0,speed * Time.deltaTime);
    }
}