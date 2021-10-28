using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineOnOff : MonoBehaviour
{
    public void OutlineOn()
    {
        gameObject.GetComponent<Outline>().enabled = true;
    }

    public void OutlineOff()
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }

    public void PlayDispatcher()
    {
        Dispatcher dispatcher = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Dispatcher>();
        dispatcher.Play();
    }
}
