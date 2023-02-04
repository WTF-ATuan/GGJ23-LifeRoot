using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public SpriteRenderer LoopBG;
    [Range(0,1)]
    public float SmoothDamp = 0.1f;
    
    private Material M;
    private Vector3 Vec;
    
    private void Awake() {
        M = LoopBG.material;
    }

    public Vector3 NowFocusPos()
    {
        var player = PlayerController.Instance;
        if (player == null) return Vector3.zero;
        var nowHaveRock = player._currentTarget != null;
        return nowHaveRock ? player._currentTarget.transform.position : player.transform.position;
    }

    private void Update()
    {
        var nowFocusPos = NowFocusPos();
        nowFocusPos = Vector3.SmoothDamp(Camera.main.transform.position, nowFocusPos, ref Vec, SmoothDamp);
        LoopBGUpdate();
        CameraUpdate();
        
        void LoopBGUpdate()
        {
            M.SetFloat("_XOffset", nowFocusPos.x);
            M.SetFloat("_YOffset", nowFocusPos.y);
        }

        void CameraUpdate()
        {
            var targetPos = nowFocusPos;
            targetPos.z = Camera.main.transform.position.z;
            Camera.main.transform.position = targetPos;
        }
    }
}
