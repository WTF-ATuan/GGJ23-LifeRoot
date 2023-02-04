using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Object linking
    public LineRenderer _rootRenderer;
    public GameObject currentTarget;
    public Rigidbody2D playerRigidbody;

    // Config
    public float addForceDotValueThreshold = 0.7f;
    public float forceFactor = 0.5f;
    public float speedLimit = 10f;

    // Reader
    public float speed;

    // Internal
    Vector3 lastPosition;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (currentTarget != null)
        {   
            Vector3 direction = currentTarget.transform.position - transform.position;
            transform.up = direction;
            UpdateVectors();
            ApplyForce();
        }
    }
    
    void UpdateVectors()
    {
        direction = transform.position - lastPosition;
        speed = direction.magnitude / Time.fixedDeltaTime;
        lastPosition = transform.position;
    }

    void Update()
    {
        DrawRoot();
    }

    void DrawRoot()
    {
        if (currentTarget != null)
        {
            _rootRenderer.enabled = true;
            _rootRenderer.SetPosition(0, transform.position);
            _rootRenderer.SetPosition(1, currentTarget.transform.position);
        }
        else
        {
            _rootRenderer.enabled = false;
        }
            
    }
    
    void ApplyForce()
    {
        float dotValue = Vector3.Dot(direction.normalized, transform.right);
        if (Math.Abs(dotValue) > addForceDotValueThreshold && speed < speedLimit)
        {
            Vector3 force = transform.right * forceFactor * dotValue;
            playerRigidbody.AddForceAtPosition(force, transform.position);
        }
    }

    void OnRootAttach()
    {
        lastPosition = transform.position;
    }
}
