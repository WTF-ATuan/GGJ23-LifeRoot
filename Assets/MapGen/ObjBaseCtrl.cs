using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjBaseCtrl : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
