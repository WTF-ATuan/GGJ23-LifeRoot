using System;
using System.Collections;
using System.Collections.Generic;
using Game.GameEvent;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UICtrl : MonoBehaviour
{

    public StringEvent TimerEvent;
    public IntEvent ScoreEvent;
    
    public TMP_Text Timer;
    public TMP_Text Score;

    private float StartTime;
    
    
    private void Start()
    {
        StartTime = Time.time;
        TimerEvent?.Register(TimerUpdate);
        ScoreEvent?.Register(ScoreUpdate);
    }

    void TimerUpdate(string e)
    {
        Timer.text = e;
    }
    
    void ScoreUpdate(int e)
    {
        Score.text = $"Score: {e}";
    }

    private void OnDestroy()
    {
        TimerEvent?.Unregister(TimerUpdate);
        ScoreEvent?.Unregister(ScoreUpdate);
    }
}
