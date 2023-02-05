using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RocketCtrl : ObjBaseCtrl
{

    public float FlyDis = 10;
    public float FlySec = 5;
    
    public UnityEvent Reset;
    public UnityEvent FlyBegin;
    public UnityEvent FlyEnd;

    
    private bool IsFly;
    private Tweener T;


    protected override void OnEnable()
    {
        base.OnEnable();
        IsFly = false;
        T?.Kill();
        transform.localPosition = Vector3.zero;
        Reset.Invoke();
    }

    public override void HookOn()
    {
        if (IsFly) return;
        T = transform.DOMove(transform.position + Vector3.up * FlyDis, FlySec).SetEase(Ease.InCirc).OnComplete(() =>
        {
            FlyEnd.Invoke();
        });
        FlyBegin.Invoke();
        IsFly = true;
    }
}
