using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GameEvent;

public class BGM_Controller : MonoBehaviour
{

    
    public AudioSource mainBgmSource;
    public AudioSource introBgmSource;
    public AudioSource deadBgmSource;
    public AudioSource winBgmSource;
    public AudioSource TitleBgmSource;

    public VoidEvent finishTutorialEvent;
    public VoidEvent winEvent;

    private List<AudioSource> AllSound;
    void Start()
    {
        finishTutorialEvent.Register(PlayMainBGM);
        winEvent.Register(PlayWinBGM);
        AllSound = new List<AudioSource>();
        AllSound.Add(mainBgmSource);
        AllSound.Add(introBgmSource);
        AllSound.Add(deadBgmSource);
        AllSound.Add(winBgmSource);
        AllSound.Add(TitleBgmSource);
    }

    private void OnDestroy()
    {
        finishTutorialEvent.Unregister(PlayMainBGM);
        winEvent.Unregister(PlayWinBGM);
    }

    void StopAll()
    {
        AllSound.ForEach(e=>e.Stop());
    }
    
    public void PlayMainBGM()
    {
        StopAll();
        mainBgmSource.Play();
    }

    public void PlayDeadBGM()
    {
        StopAll();
        deadBgmSource.Play();
    }

    public void PlayWinBGM()
    {
        StopAll();
        winBgmSource.Play();
    }
}
