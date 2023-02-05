using UnityEngine;
using Game.GameEvent;

public class GameController : MonoBehaviour {
    #region Varibles

    [SerializeField]
    private IntEvent scoreEvent;
    [SerializeField]
    private FloatEvent timeEvent;
    [SerializeField]
    private StringEvent timeFormatEvent;

    private GameObject player;
    private float gameTime = 0;

    #endregion

    #region Behaviour

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        if (player == null)
            return;

        gameTime += Time.deltaTime;

        var score = (int)player.transform.position.y;

        scoreEvent.Raise(score);
        timeEvent.Raise(gameTime);
        timeFormatEvent.Raise(string.Format("%dm%ds", gameTime / 60, gameTime % 60));
    }

    #endregion
}
