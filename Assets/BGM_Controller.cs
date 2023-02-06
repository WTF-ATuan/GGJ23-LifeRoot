using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GameEvent;
using UniRx;

public class BGM_Controller : MonoBehaviour
{

    public BoolEvent MusicRock;
    public AudioSource mainBgmSource;
    public AudioSource mainBgm2Source;
    public AudioSource introBgmSource;
    public AudioSource deadBgmSource;
    public AudioSource winBgmSource;
    public AudioSource TitleBgmSource;

    public VoidEvent finishTutorialEvent;
    public VoidEvent winEvent;

    private List<AudioSource> AllSound;

    private void Awake()
    {
        AllSound = new List<AudioSource>();
        AllSound.Add(mainBgmSource);
        AllSound.Add(mainBgm2Source);
        AllSound.Add(introBgmSource);
        AllSound.Add(deadBgmSource);
        AllSound.Add(winBgmSource);
        AllSound.Add(TitleBgmSource);
        
        finishTutorialEvent.Register(PlayMainBGM);
        winEvent.Register(PlayWinBGM);
        MusicRock.Register(mainBgm2SourceSwitch);
    }

    void mainBgm2SourceSwitch(bool active)
    {
        mainBgm2Source.volume = active ? 1 : 0;
    }

    private void OnDestroy()
    {
        finishTutorialEvent.Unregister(PlayMainBGM);
        winEvent.Unregister(PlayWinBGM);
        MusicRock.Unregister(mainBgm2SourceSwitch);
    }

    void StopAll()
    {
        AllSound.ForEach(e=>e.Stop());
    }
    
    public void PlayMainBGM()
    {
        StopAll();
        mainBgmSource.Play();
        mainBgm2Source.Play();
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

public class OnMusicRockHook
{
    public bool Hook;

    public OnMusicRockHook(bool hook)
    {
        Hook = hook;
    }
}
