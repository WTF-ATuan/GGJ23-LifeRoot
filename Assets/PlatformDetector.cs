using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GameEvent;

public class PlatformDetector : MonoBehaviour
{

    public float hitPlatformMinInterval = 1f;
    public VoidEvent winEvent;

    DateTime lastHit = DateTime.Now;

    public PlayerController playerController;
    Collider2D[] collidedResults = new Collider2D[100];
    Collider2D detectCollider;
    ContactFilter2D collideFilter;

    bool detectEnabled = true;


    // Start is called before the first frame update
    void Start()
    {

        detectCollider = GetComponent<Collider2D>();

        collideFilter = new ContactFilter2D();
        collideFilter.useLayerMask = true;
        collideFilter.layerMask = LayerMask.GetMask("Platform");


        winEvent.Register(OnWin);


    }
    // Update is called once per frame
    void Update()
    {
        if (detectEnabled)
        {
            UpdateCollide();
        }
    }

    void UpdateCollide()
    {
        float lastHitTimeDiff = (float)((DateTime.Now - lastHit).TotalSeconds);
        if (lastHitTimeDiff > hitPlatformMinInterval)
        {
            int size = detectCollider.OverlapCollider(collideFilter, collidedResults);
            if (size > 0)
            {
                OnPlatformHit();
            }
        }

    }

    void OnPlatformHit()
    {
        lastHit = DateTime.Now;
        //playerController.OnRootDetach();
        Debug.Log("Hit platform");
    }

    void OnWin()
    {
        detectEnabled = false;
    }

}
