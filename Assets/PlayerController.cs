using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject currentTarget;
    public float addForceDotValueThreshold = 0.7f;
    public float forceFactor = 1f;
    public Rigidbody2D playerRigidbody;

    Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentTarget != null)
        {   
            Vector3 direction = currentTarget.transform.position - transform.position;
            transform.up = direction;
            ApplyForce();
            lastPosition = transform.position;
        }
    }
    
    void ApplyForce()
    {
        Vector3 direction = (transform.position - lastPosition).normalized;
        float dotValue = Vector3.Dot(direction, transform.right);
        Debug.Log("Dot: " + dotValue);
        if (Math.Abs(dotValue) > addForceDotValueThreshold)
        {
            Debug.Log("Apply force");
            Vector3 force = transform.right * forceFactor * dotValue;
            playerRigidbody.AddForceAtPosition(force, transform.position);
        }
    }

    void OnRootAttach()
    {
        lastPosition = transform.position;
    }
}
