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

    public VoidEvent finishTutorialEvent;
    public VoidEvent winEvent;

    // Start is called before the first frame update
    void Start()
    {
        finishTutorialEvent.Register(PlayMainBGM);
        winEvent.Register(PlayWinBGM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMainBGM()
    {
        if(mainBgmSource != null)
        {
            mainBgmSource.Play();
        }
        if(introBgmSource != null) {
            introBgmSource.Stop();
        }
    }

    public void PlayDeadBGM()
    {
        if (mainBgmSource != null)
        {
            mainBgmSource.Stop();
        }
        if (deadBgmSource != null) {
            deadBgmSource.Play();
        }
    }

    public void PlayWinBGM()
    {
        if (mainBgmSource != null)
        {
            mainBgmSource.Stop();
        }

        if (winBgmSource != null) {
            winBgmSource.Play();
        }
    }
}
