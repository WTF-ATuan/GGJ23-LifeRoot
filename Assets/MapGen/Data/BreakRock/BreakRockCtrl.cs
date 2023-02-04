using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BreakRockCtrl : ObjBaseCtrl
{
    public float LifeTime;

    private float RestLifeTime;
    private SpriteRenderer Sprite;
    
    protected override void Awake()
    {
        base.Awake();
        Sprite = GetComponent<SpriteRenderer>();

    }

    private void Start() {
        RestLifeTime = LifeTime;
    }

    private void Update()
    {
        if (IsHooking) {
            RestLifeTime -= Time.deltaTime;
            var color = Sprite.color;
            color.a = RestLifeTime / LifeTime;
            Sprite.color = color;
            if (RestLifeTime < 0) {
                OnDie();
            }
        }
    }

    void OnDie()
    {
        MapGenCtrl.Instance.RecycleChunk(MyChunk);
        MapGenCtrl.Instance.ChangeChunkType(MyChunk, ObjType.Null);
        EventAggregator.Publish(new RockBreak(gameObject));
    }
}
