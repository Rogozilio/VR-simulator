using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private float button = 0;

    public float Buttone
    {
        get => button;
        set => button = value;
    }

    public void Hello()
    {
        Debug.Log("Hello world");
    }

    public IEnumerator Corut()
    {
        Debug.Log("start corutine");
        yield break;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            button = 1;
            Debug.Log("asda");
        }
    }
}