using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LoopBGCtrl : MonoBehaviour
{
    public Transform Target;
    private Material M;

    private void Awake()
    {
        M = GetComponent<SpriteRenderer>().material;
    }

    public void Update()
    {
        Vector3 pos = Target.position;
        M.SetFloat("_XOffset", pos.x);
        M.SetFloat("_YOffset", pos.y);
    }
    
}
