using UnityEngine;

public class AutoMove : MonoBehaviour {
    #region Variables

    public Transform target;
    public bool isMoveX;
    public bool isMoveY;

    #endregion

    #region Behaviour

    private void Update() {
        if (target == null)
            return;

        var position = transform.position;

        if (isMoveX)
            position.x = target.position.x;

        if (isMoveY)
            position.y = target.position.y;

        transform.position = position;
    }

    #endregion
}
