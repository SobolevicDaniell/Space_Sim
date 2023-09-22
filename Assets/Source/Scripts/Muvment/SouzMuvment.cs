using System.Collections;
using System.Collections.Generic;using Source.Scripts.Muvment;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SouzMuvment : MonoBehaviour , IMuvment
{
    [SerializeField] private float rollForse;
    [SerializeField] private float pitchForse;
    [SerializeField] private float yawForse;

    [SerializeField] private float forwardForse;
    [SerializeField] private float backForse;
    [SerializeField] private float horizontalForse;
    [SerializeField] private float verticalForse;
    [SerializeField] private float stabilizotionForse;

    private Rigidbody rb;
    private Vector3 dir = Vector3.up;

    private bool IsStabilization = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Muvment();
        Stabilizator();
    }

    void FixedUpdate()
    {
        

        Vector3 dir = transform.TransformDirection(Vector3.forward);
    }

    void Muvment()
    {
        //Vector3 forceDirection = transform.TransformDirection(Vector3.forward);
        //Vector3 torque = Vector3.Cross(forceDirection, rb.centerOfMass - transform.position);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(new Vector3(0, 0, forwardForse), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        { 
            rb.AddRelativeForce(new Vector3(0, 0, -backForse), ForceMode.Impulse);
        }
        

        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddRelativeForce(new Vector3(horizontalForse, 0, 0), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddRelativeForce(new Vector3(-horizontalForse, 0, 0), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddRelativeForce(new Vector3(0, verticalForse, 0), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddRelativeForce(new Vector3(0, -verticalForse, 0), ForceMode.Impulse);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.AddTorque(transform.forward * rollForse, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.AddTorque(transform.forward * -rollForse, ForceMode.Impulse);
        }
        
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //rb.AddTorque(pitchForse, 0, 0, ForceMode.Impulse);
            //rb.transform.Rotate(pitchForse, 0, 0, Space.Self);
            rb.AddTorque(transform.right * pitchForse, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //rb.AddTorque(-pitchForse, 0, 0, ForceMode.Impulse);
            //rb.transform.Rotate(-pitchForse, 0, 0, Space.Self);
            rb.AddTorque(transform.right * -pitchForse, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //rb.AddTorque(0, -yawForse, 0, ForceMode.Impulse);
            //rb.transform.Rotate(0, -yawForse, 0, Space.Self);
            rb.AddTorque(transform.up * -yawForse, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //rb.AddTorque(0, yawForse, 0, ForceMode.Impulse);
            rb.AddTorque(transform.up * yawForse, ForceMode.Impulse);
        }   
    }

    
    
    void Stabilizator()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (IsStabilization)
            {
                rb.drag = 0;
                rb.angularDrag = 0;
                Debug.Log("Stab off");
                IsStabilization = false;
            }
            else
            {
                rb.drag = stabilizotionForse;
                rb.angularDrag = stabilizotionForse;
                Debug.Log("Stab on");
                IsStabilization = true;
            }
        }
    }
}

