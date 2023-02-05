using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public SpriteRenderer LoopBG;
    public SpriteRenderer Ground;
    [Range(0,1)]
    public float SmoothDamp = 0.1f;
    
    private Material SkyM;
    private Material GroundM;
    private Vector3 Vec;
    
    private void Awake() {
        SkyM = LoopBG.material;
        GroundM = Ground.material;
    }

    public Vector3 NowFocusPos()
    {
        var player = PlayerController.Instance;
        if (player == null) return Vector3.zero;
        var nowHaveRock = player._currentTarget != null;
        return nowHaveRock ? player._currentTarget.transform.position : player.playerTransform.position;
    }

    private void Update()
    {
        var nowFocusPos = NowFocusPos();
        nowFocusPos = Vector3.SmoothDamp(Camera.main.transform.position, nowFocusPos, ref Vec, SmoothDamp);
        LoopBGUpdate();
        CameraUpdate();
        GroundUpdate();
        
        void LoopBGUpdate()
        {
            SkyM.SetFloat("_XOffset", nowFocusPos.x);
            SkyM.SetFloat("_YOffset", nowFocusPos.y);
        }

        void CameraUpdate()
        {
            var targetPos = nowFocusPos;
            targetPos.z = Camera.main.transform.position.z;
            Camera.main.transform.position = targetPos;
        }

        void GroundUpdate()
        {
            var pos = Ground.transform.position;
            pos.x = nowFocusPos.x;
            Ground.transform.position = pos;
            GroundM.SetFloat("_XOffset", nowFocusPos.x);
        }
    }
}
