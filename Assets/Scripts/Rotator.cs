using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rotator : MonoBehaviour
{
    public GameObject RotatingPart;

    //private bool areValuesSet = false;
    private bool canChangePressure = false;
    //private int direction = 0;
    private float angleBoundsDelta = 5f;
    private CircularDrive cd;

    void Start()
    {
        cd = gameObject.GetComponent(typeof(CircularDrive)) as CircularDrive;
    }

    void Update()
    {

        if ((cd.outAngle < cd.minAngle) || (cd.outAngle > cd.maxAngle))
        {
            // Error: angle out of bounds
        }
        else
        {
            if (cd.outAngle <= cd.minAngle + angleBoundsDelta)
            {
                if (canChangePressure)
                {
                    if (RotatingPart.transform.eulerAngles.y < 90)
                    {
                        RotatingPart.transform.eulerAngles = new Vector3(0, RotatingPart.transform.eulerAngles.y + 2f, 0);
                    }
                }
                canChangePressure = false;
            }
            else if (cd.outAngle >= cd.maxAngle - angleBoundsDelta)
            {
                canChangePressure = true;
            }
            //else if ((cd.outAngle >= (cd.maxAngle + cd.minAngle) / 2f - angleBoundsDelta) && (cd.outAngle <= (cd.maxAngle + cd.minAngle) / 2f + angleBoundsDelta))
            //{
            //    canChangePressure = true;
            //}
        }
    }

    public void OnMinAngle()
    {
        //canChangePressure = true;
    }

    public void OnMaxAngle()
    {
        //if (canChangePressure)
        //{
        //    PMeter.TargetPressure = PMeter.TargetPressure + 1f * direction;
        //}
        //canChangePressure = false;
    }
}
