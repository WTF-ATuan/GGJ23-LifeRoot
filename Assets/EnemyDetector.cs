using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GameEvent;
using DG.Tweening;

public class EnemyDetector : MonoBehaviour
{

    public float maxScale = 4f;
    public float hitEnemyMinInterval = 1f;
    public float rootRecoverTime = 2f;
    public VoidEvent winEvent;

    DateTime lastHit = DateTime.Now;

    public PlayerController playerController;
    Collider2D[] collidedResults = new Collider2D[100];
    Collider2D detectCollider;
    ContactFilter2D enemyFilter;

    public float currentScale;


    public AudioSource audioSource;
    public AudioClip hitClip;

    private Sequence scaleSequence;
    bool detectEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        currentScale = maxScale;
        audioSource = GetComponent<AudioSource>();

        detectCollider = GetComponent<Collider2D>();

        enemyFilter = new ContactFilter2D();
        enemyFilter.useLayerMask = true;
        enemyFilter.layerMask = LayerMask.GetMask("Enemy");

        scaleSequence = DOTween.Sequence();
        scaleSequence.SetAutoKill(false);

        winEvent.Register(OnWin);


    }
    // Update is called once per frame
    void Update()
    {
        if (detectEnabled)
        {
            UpdateEnemy();
        }
    }

    void FixedUpdate()
    {
        UpdateScale();
    }

    void UpdateScale()
    {
        transform.parent.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    void UpdateEnemy()
    {
        float lastHitTimeDiff = (float)((DateTime.Now - lastHit).TotalSeconds);
        if (lastHitTimeDiff > hitEnemyMinInterval)
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
        if(audioSource != null) {
            audioSource.PlayOneShot(hitClip, 0.4f);
        }
        
        Debug.Log("Hit enemy");
    }

    void OnWin()
    {
        audioSource.Stop();
        detectEnabled = false;
    }
}
