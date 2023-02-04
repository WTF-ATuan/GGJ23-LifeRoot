using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UICtrl : MonoBehaviour
{
    public TMP_Text Timer;
    public TMP_Text High;
    public TMP_Text Score;

    private float StartTime;
    
    
    private void Start()
    {
        StartTime = Time.time;
    }

    private void Update()
    {
        Timer.text = $"{ Time.time - StartTime}";
    }
}
