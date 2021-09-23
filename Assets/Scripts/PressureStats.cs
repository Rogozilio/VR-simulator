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
        int pi = rnd.Next(400, 600 + 1);
        int sp = rnd.Next(0, 300 + 1);
        int tp;
        do
        {
            tp = rnd.Next(100, pi - 100 + 1);
        } while (tp == sp);
        startPressureIN = pi / 100f;
        startPressureOUT = sp / 100f;
        targetPressureOUT = tp / 100f;
    }
}
