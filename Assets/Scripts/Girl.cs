using UnityEngine;

public class Girl : MonoBehaviour {
    #region Variables

    public Rigidbody2D testRigidbody;

    [SerializeField]
    private SpringJoint2D leftHandCatcher;
    [SerializeField]
    private SpringJoint2D rightHandCatcher;

    [SerializeField]
    private FixedJoint2D bodyCatcher;

    #endregion

    #region Public Function

    public void Catch(Rigidbody2D rigidbody) {
        leftHandCatcher.connectedBody = rigidbody;
        rightHandCatcher.connectedBody = rigidbody;

        var flag = rigidbody != null;

        leftHandCatcher.enabled = flag;
        rightHandCatcher.enabled = flag;
    }

    public void NailBodyTo(Rigidbody2D rigidbody) {
        bodyCatcher.connectedBody = rigidbody;
    }

    #endregion

    #region Behaviour

    private void Start() {
        Catch(testRigidbody);
    }

    #endregion
}
