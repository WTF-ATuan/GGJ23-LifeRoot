using Game.GameEvent;
using TMPro;
using UnityEngine;

public class UICtrl : MonoBehaviour {

    public FloatEvent timeEvent;
    public IntEvent distanceEvent;

    public TMP_Text timeText;
    public TMP_Text distanceText;


    private void Start() {
        if( timeEvent )
            timeEvent.Register(TimerUpdate);

        if( distanceEvent )
            distanceEvent.Register(UpdateDistance);
    }

    void TimerUpdate(float time) {
        timeText.text = string.Format("%.2d:%.2d", (int)time / 60, (int)time % 60);
    }

    void UpdateDistance(int distance) {
        distanceText.text = $"Ending Distance: {distance}";
    }

    private void OnDestroy() {
        if( timeEvent )
            timeEvent.Unregister(TimerUpdate);

        if( distanceEvent )
            distanceEvent.Unregister(UpdateDistance);
    }
}
