using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PressureStats : MonoBehaviour
{
    [SerializeField]
    private float startPressureIN;
    public float StartPressureIN { get { return startPressureIN; } }
    [SerializeField]
    private float startPressureOUT;
    public float StartPressureOUT { get { return startPressureOUT; } }
    [SerializeField]
    private float targetPressureOUT;
    public float TargetPressureOUT { get { return targetPressureOUT; } }

    void Start()
    {
        System.Random rnd = new System.Random();
        startPressureIN = rnd.Next(20, 25 + 1);
        int sp = rnd.Next(5, 19 + 1);
        int tp = 0;
        do
        {
            tp = rnd.Next(7, 15 + 1);
        } while (tp == sp);
        startPressureOUT = sp;
        targetPressureOUT = tp;
    }
}
