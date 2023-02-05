using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISpace : MonoBehaviour
{
    private Button B;
    private void Awake()
    {
        B = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            B.onClick.Invoke();
        }
    }
}
