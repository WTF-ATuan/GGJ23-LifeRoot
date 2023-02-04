using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachDetectorController : MonoBehaviour
{
    public GameObject currentTarget;

    Collider2D[] collidedResults = new Collider2D[100];
    Collider2D detectCollider;
    ContactFilter2D filter;
    // Start is called before the first frame update
    void Start()
    {
        detectCollider = GetComponent<Collider2D>();
        filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.layerMask = LayerMask.GetMask("Attachable");
    }

    void Update()
    {
        UpdateCurrentTarget();
    }

    void UpdateCurrentTarget()
    {
        GameObject target = null;
        float minDistance = Mathf.Infinity;
        int size = detectCollider.OverlapCollider(filter, collidedResults);
        for(int i = 0; i < size; i++)
        {
            Collider2D targetCollider = collidedResults[i];
            float distance = (targetCollider.gameObject.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                target = targetCollider.gameObject;
                minDistance = distance;
            }

        }
        currentTarget = target;
    }
}
