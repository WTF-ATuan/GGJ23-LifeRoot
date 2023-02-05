using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttachDetectorController : MonoBehaviour
{
    public GameObject currentTarget;
    public float maxScale = 20f;
    public float hitEnemyMinInterval = 1f;
    public float rootRecoverTime = 2f;
    public PlayerController playerController;

    DateTime lastHit = DateTime.Now;

    public float currentScale;

    Collider2D[] collidedResults = new Collider2D[100];
    Collider2D detectCollider;
    ContactFilter2D attachFilter;
    ContactFilter2D enemyFilter;

    private Sequence scaleSequence;
        
    // Start is called before the first frame update
    void Start()
    {
        currentScale = maxScale;
        detectCollider = GetComponent<Collider2D>();
        attachFilter = new ContactFilter2D();
        attachFilter.useLayerMask = true;
        attachFilter.layerMask = LayerMask.GetMask("Attachable");

        enemyFilter = new ContactFilter2D();
        enemyFilter.useLayerMask = true;
        enemyFilter.layerMask = LayerMask.GetMask("Enemy");

        scaleSequence = DOTween.Sequence();
        scaleSequence.SetAutoKill(false);
        

    }

    void Update()
    {
        UpdateCurrentTarget();
        UpdateEnemy();
    }

    void FixedUpdate()
    {
        UpdateScale();
    }

    void UpdateScale()
    {
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    void UpdateCurrentTarget()
    {
        GameObject target = null;
        float minDistance = Mathf.Infinity;
        int size = detectCollider.OverlapCollider(attachFilter, collidedResults);
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

    void UpdateEnemy()
    {
        float lastHitTimeDiff = (float)((DateTime.Now - lastHit).TotalSeconds);
        if(lastHitTimeDiff > hitEnemyMinInterval)
        {
            int size = detectCollider.OverlapCollider(enemyFilter, collidedResults);
            if (size > 0)
            {
                OnEnemyHit();
            }
        }
        
    }

    void OnEnemyHit()
    {
        lastHit = DateTime.Now;
        scaleSequence.Append(DOTween.To(delegate (float value) {
            currentScale = value;
        }, 0, maxScale, rootRecoverTime).SetEase(Ease.InCirc));
        playerController.OnRootDetach();
    }
}
