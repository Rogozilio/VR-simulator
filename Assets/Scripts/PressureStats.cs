using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureStats : MonoBehaviour
{
    private float startPressureIN;
    public float StartPressureIN { get { return startPressureIN; } }
    [SerializeField]
    private float customersNumber;
    public float CustomersNumber { get { return customersNumber; } set { customersNumber = value; } }

    void Awake()
    {
        System.Random rnd = new System.Random();
        startPressureIN = rnd.Next(2900, 3900 + 1) / 1000f;
        customersNumber = rnd.Next(100, 1000 + 1);
    }
}
