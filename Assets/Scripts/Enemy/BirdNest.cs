using UnityEngine;

public class BirdNest : MonoBehaviour {
    #region Variables

    [SerializeField]
    private GameObject bird;
    [SerializeField]
    private float maxShowTime = 4;
    [SerializeField]
    private float minShowTime = 1;
    [SerializeField]
    private CooldownTime cooldownTime;

    #endregion

    #region Public Function

    public void RandomShowTime() {
        var cd = Random.Range(minShowTime, maxShowTime);

        cooldownTime.cdTime = cd;

        cooldownTime.ResetCooldown();
    }

    #endregion

    #region Behaviour

    private void OnEnable() {
        if( bird.activeSelf )
            cooldownTime.Stop();
        else
            RandomShowTime();        
    }

    #endregion
}
