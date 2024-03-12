using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docking : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private GameObject _dock;
    private bool col;
    private bool isDock = false;
    
    private Rigidbody rb;
    void Start()
    {
        rb = _gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && col && !isDock)
        {
            // Debug.Log("Parking");
            rb.isKinematic = true;
            _gameObject.transform.SetParent(_dock.transform);
            isDock = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && col && isDock)
        {
            rb.isKinematic = false;
            _gameObject.transform.SetParent(null);
            isDock = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dock")
        {
            Debug.Log("Collision");
            col = true;
            _dock = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Dock")
        {
            // Debug.Log("Collision Off");
            col = false;
            _dock = null;
        }
    }
}
