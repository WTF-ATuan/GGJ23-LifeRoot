using UnityEngine;
using UnityEngine.Events;

public class CooldownTime : MonoBehaviour {
    #region Variables

    public float cdTime = 2.0f;
    public UnityEvent timeEvent;

    private float time = 0;

    #endregion

    #region Public Function

    public void ResetCooldown() {
        time = 0;
    }

    public void Stop() {
        time = cdTime + 1;
    }

    #endregion

    #region Behaviour

    private void Awake() {
        if (timeEvent == null)
            timeEvent = new UnityEvent();
    }

    private void OnEnable() {
        ResetCooldown();
    }

    private void Update() {
        if (time >= cdTime)
            return;

        time += Time.deltaTime;

        if (time < cdTime)
            return;

        timeEvent.Invoke();
    }

    #endregion
}
