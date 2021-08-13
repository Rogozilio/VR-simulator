using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LeverMinMaxCheck : MonoBehaviour
{
    public PressureStats PS;
    public PMeterArrowRotation PMeter;

    private bool areValuesSet = false;
    private bool canChangePressure = true;
    private int direction = 0;
    private float angleBoundsDelta = 5f;
    private CircularDrive cd;

    void Start()
    {
        cd = gameObject.GetComponent(typeof(CircularDrive)) as CircularDrive;
    }

    void Update()
    {
        if (!areValuesSet)
        {
            if (PS.TargetPressureOUT > PS.StartPressureOUT)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            areValuesSet = true;
        }

        if ((cd.outAngle < cd.minAngle) || (cd.outAngle > cd.maxAngle))
        {
            // Error: angle out of bounds
        }
        else
        {
            if (cd.outAngle <= cd.minAngle + angleBoundsDelta)
            {
                canChangePressure = true;
            }
            else if (cd.outAngle >= cd.maxAngle - angleBoundsDelta)
            {
                if (canChangePressure)
                {
                    PMeter.TargetPressure = PMeter.TargetPressure + 1f * direction;
                }
                canChangePressure = false;
            }
        }
    }

    public void OnMinAngle()
    {
        canChangePressure = true;
    }

    public void OnMaxAngle()
    {
        if (canChangePressure)
        {
            PMeter.TargetPressure = PMeter.TargetPressure + 1f * direction;
        }
        canChangePressure = false;
    }
}
