using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenCtrl : MonoBehaviour
{
    public GameObject FolowTarget;
    public SpriteRenderer BG;
    public List<GenObjBaseCtrl> GenObjList;

    private float LastY;

    private void Update()
    {
        float nowY = FolowTarget.transform.position.y;
        if (nowY > LastY) {
            LastY = nowY;
            YUpdate();
        }
    }

    void YUpdate()
    {
        CameraUpdate();
        BGUpdate();

        void CameraUpdate()
        {
            Vector3 cameraPos = Camera.main.transform.position;
            cameraPos.y = LastY;
            Camera.main.transform.position = cameraPos;
        }

        void BGUpdate() {
            BG.material.SetFloat("_Offset",LastY);
        }
        
    }
}
