using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class BreakRockCtrl : ObjBaseCtrl
{
    public float LifeTime;

    private float RestLifeTime;
    private float LastLifeTime;
    public SpriteRenderer Sprite;

    public UnityEvent AlmostBreak;
    public UnityEvent OnBreak;

    private void Start() {
        RestLifeTime = LifeTime;
    }

    private void Update()
    {
        if (IsHooking) {
            RestLifeTime -= Time.deltaTime;
            float nowLifeTime = RestLifeTime / LifeTime;
            
            
            var color = Sprite.color;
            color.a = 1-nowLifeTime;
            Sprite.color = color;
            
            
            if (LastLifeTime > 0.5 && nowLifeTime < 0.5) AlmostBreak.Invoke();
            if (LastLifeTime > 0 && nowLifeTime < 0) OnDie();
            
            
            LastLifeTime = nowLifeTime;
        }
    }

    void OnDie() {
        Do();
        
        async Task Do() {
            MapGenCtrl.Instance.ChangeChunkType(MyChunk, ObjType.Null);
            EventAggregator.Publish(new BreakRoot(gameObject));
            OnBreak.Invoke();
            await Task.Delay(500);
            MapGenCtrl.Instance.RecycleChunk(MyChunk);
        }
    }
}
