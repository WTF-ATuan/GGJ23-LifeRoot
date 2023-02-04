using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class GameCtrl : MonoBehaviour
{
    private const string KEY_RESTART = "Restart";
    
    public UnityEvent OnReStart;
    public UnityEvent OnFirstOpen;
    private void Awake()
    {
        if (PlayerPrefs.GetInt(KEY_RESTART) == 1) {
            OnReStart?.Invoke();
            PlayerPrefs.SetInt(KEY_RESTART, 0);
        } else {
            OnFirstOpen?.Invoke();
        }
    }

    public void B_OnPause()
    {
        _ = d();
        
        async Task d()
        {
            await Task.Delay(201);
            Time.timeScale = 0;
        }
    }
    
    public void B_OnContinue()
    {
        Time.timeScale = 1;
    }

    public void B_Restart()
    {
        PlayerPrefs.SetInt(KEY_RESTART, 1);
        Application.LoadLevel(0);
    }
}
