using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PanelController : MonoBehaviour
{
    public GameObject PSText;
    private float pressure;
    public float Pressure { get { return pressure; } set { pressure = value; } }
    private ManometrData md;
    public ManometrData MD { get { return md; } set { md = value; } }

    // Start is called before the first frame update
    public void Close()
    {
        Destroy(gameObject);
        md.hasPannel = false;
    }

    public void Transfer()
    {

    }

    void Update()
    {
        PSText.GetComponent<Text>().text = (Math.Round(pressure, 2).ToString() + " MPa");
    }
}
