using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
