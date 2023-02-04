using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{

    public GameObject currentTarget;
    LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if (currentTarget != null)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, currentTarget.transform.position);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
