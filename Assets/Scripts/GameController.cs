using UnityEngine;
using Game.GameEvent;

public class GameController : MonoBehaviour {
    #region Varibles

    [SerializeField]
    private IntEvent distanceEvent;
    [SerializeField]
    private FloatEvent timeEvent;
    [SerializeField]
    private StringEvent timeFormatEvent;
    [SerializeField]
    private VoidEvent finishGameEvent;

    private GameObject player;
    private GameObject endingFlag;
    private float gameTime = 0;

    private bool isFinish = false;

    #endregion

    #region Private Function

    private void UpdateTime() {
        gameTime += Time.deltaTime;

        timeEvent.Raise(gameTime);
        timeFormatEvent.Raise(string.Format("%dm%ds", gameTime / 60, gameTime % 60));
    }

    private void UpdateDistance() {
        if (endingFlag == null)
            return;

        var distance = (int)(endingFlag.transform.position.y -  player.transform.position.y);

        distanceEvent.Raise(distance);
    }

    private void CheckEnding() {
        if (endingFlag == null )
            return;

        if (player.transform.position.y < endingFlag.transform.position.y)
            return;

        finishGameEvent.Raise();

        isFinish = true;
    }

    #endregion

    #region Behaviour

    private void Start() {
        player = GameObject.FindWithTag("Player");
        endingFlag = GameObject.FindWithTag("Finish Game");

        if (endingFlag.TryGetComponent(out AutoMove autoMove)) {
            autoMove.target = player.transform;
        }
    }

    private void Update() {
        if (player == null)
            return;

        if (isFinish)
            return;

        UpdateTime();
        UpdateDistance();
        CheckEnding();
    }

    #endregion
}
