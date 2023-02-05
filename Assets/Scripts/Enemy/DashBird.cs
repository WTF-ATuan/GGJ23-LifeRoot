using UnityEngine;

public class DashBird : MonoBehaviour  {
    #region Variables

    public float speed = 1.0f;
    public bool canTurn = true;

    [SerializeField]
    private SpriteRenderer body;

    private GameObject player;

    #endregion

    #region Property

    public bool CanTurn {
        get { return canTurn; }
        set { canTurn = value; }
    }

    #endregion

    #region Bhaviour

    private void Awake() {
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable() {
        if (player == null)
            return;

        var goRight = Mathf.Sign(player.transform.position.x - transform.position.x);

        speed = Random.Range(1.0f, 2.0f) * goRight;

        canTurn = false;
    }

    private void Update() {
        TryTurn();

        transform.Translate(speed * Time.deltaTime, 0, 0);

        body.flipX = speed > 0;
    }

    private void TryTurn() {
        if (!canTurn)
            return;

        var playerPosition = player.transform.position;
        var myPosition = transform.position;

        if (Mathf.Abs(playerPosition.y - myPosition.y) > 1)
            return;

        speed = Mathf.Abs(speed) * Mathf.Sign(playerPosition.x - myPosition.x);

        canTurn = false;
    }

    #endregion

}
