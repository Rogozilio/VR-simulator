using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManometrData : MonoBehaviour
{
    public GameObject Arrow;
    private float pressure;
    public float xAdd;
    public float yAdd;
    public float zAdd;
    private PMeterArrowRotation PMAR;
    public bool hasPannel;

    public float Pressure { get { return pressure; } set { pressure = value; } }

    // Start is called before the first frame update
    void Start()
    {
        PMAR = Arrow.GetComponent<PMeterArrowRotation>();
        hasPannel = false;
    }

    // Update is called once per frame
    void Update()
    {
        pressure = PMAR.CurrentPressure;
    }

    public void PanelDestroyed()
    {
        hasPannel = false;
    }
}
