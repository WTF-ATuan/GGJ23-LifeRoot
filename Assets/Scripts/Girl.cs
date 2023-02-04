using UnityEngine;

public class Girl : MonoBehaviour {
    #region Enum

    public enum Emote {
        Normal, // 一般
        Cry, // 哭泣
        CryAndLaugh, // 哭笑不得
    }

    #endregion

    #region Variables

    [SerializeField]
    private SpringJoint2D leftHandCatcher;
    [SerializeField]
    private SpringJoint2D rightHandCatcher;

    [SerializeField]
    private FixedJoint2D bodyCatcher;

    [SerializeField]
    private SpriteRenderer faceRenderer;
    [SerializeField]
    private Sprite[] faceSprites;

    #endregion

    #region Property

    /// <summary>
    /// 設定表情
    /// </summary>
    public Emote FaceEmote {
        set {
            faceRenderer.sprite = faceSprites[(int)value];
        }
    }

    #endregion

    #region Public Function

    /// <summary>
    /// 雙手抓住物體(動作呈現)
    /// </summary>
    /// <param name="rigidbody">目標物體(例如繩子)</param>
    public void Catch(Rigidbody2D rigidbody) {
        leftHandCatcher.connectedBody = rigidbody;
        rightHandCatcher.connectedBody = rigidbody;

        var flag = rigidbody != null;

        leftHandCatcher.enabled = flag;
        rightHandCatcher.enabled = flag;
    }

    /// <summary>
    /// 將身體固定
    /// </summary>
    /// <param name="rigidbody">目標物(角色實體)</param>
    public void NailBodyTo(Rigidbody2D rigidbody) {
        bodyCatcher.connectedBody = rigidbody;
    }

    #endregion

    #region Behaviour

    private void Start() {
        FaceEmote = Emote.CryAndLaugh;
    }

    #endregion
}
