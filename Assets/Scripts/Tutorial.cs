using UnityEngine;
using UnityEngine.UI;
using Game.GameEvent;

public class Tutorial : MonoBehaviour {

    public AudioSource introBgmSource;
    public AudioSource titleBgmSource;
    #region Variables

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Image image;
    [SerializeField]
    private VoidEvent finishToturialEvent;

    private int spriteIndex = 0;

    #endregion

    #region Behaviour

    private void OnEnable() {
        image.gameObject.SetActive(true);
    }

    private void OnDisable() {
        image.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1))
        {
            --spriteIndex;
            if(spriteIndex == 0)
            {
                PlayTitle();
            }
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            ++spriteIndex;
            if(spriteIndex == 1)
            {
                PlayIntro();
            }
        }
            

        if (spriteIndex >= sprites.Length)
        {
            Skip();
            return;
        }

        if (spriteIndex < 0)
            spriteIndex = 0;

        image.sprite = sprites[spriteIndex];
    }

    void PlayTitle() 
    {
        titleBgmSource.Play();
        introBgmSource.Stop();
    } 

    void PlayIntro()
    {
        titleBgmSource.Stop();
        introBgmSource.Play();
    }

    public void Skip()
    {        
        enabled = false;
        image.gameObject.SetActive(false);
        finishToturialEvent.Raise();
    }

    #endregion
}
