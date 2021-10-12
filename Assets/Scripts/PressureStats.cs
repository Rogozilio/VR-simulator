using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PressureStats : MonoBehaviour
{
    [SerializeField]
    private float startPressureIN;
    public float StartPressureIN { get { return startPressureIN; } }
    //[SerializeField]
    //private float startPressureOUT;
    //public float StartPressureOUT { get { return startPressureOUT; } }
    //[SerializeField]
    //private float targetPressureOUT;
    //public float TargetPressureOUT { get { return targetPressureOUT; } }
    [SerializeField]
    private float customersNumber;
    public float CustomersNumber { get { return customersNumber; } }

    void Start()
    {
        System.Random rnd = new System.Random();
        startPressureIN = rnd.Next(2900, 3900 + 1) / 1000f;
        //int sp = rnd.Next(200, 1000 + 1);
        //int tp = 0;
        //do
        //{
        //    tp = rnd.Next(595, 605 + 1);
        //} while ((tp - 100 <= sp) && (tp + 100 >= sp));
        //startPressureOUT = sp / 1000f;
        //targetPressureOUT = tp / 1000f;
        customersNumber = rnd.Next(100, 1000 + 1);
    }
}
