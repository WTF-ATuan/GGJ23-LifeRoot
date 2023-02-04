using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class RocketCtrl : ObjBaseCtrl
{

    public float FlyDis = 10;
    public float FlySec = 5;
    
    public override void HookOn()
    {
        transform.DOMove(transform.position + Vector3.up * FlyDis, FlySec).SetEase(Ease.InCirc);
    }
}
