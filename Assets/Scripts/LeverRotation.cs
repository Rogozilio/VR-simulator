using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LeverRotation : MonoBehaviour
{
    public Rotator rotatorObject;

    private bool canChangePressure = true;
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
                    rotatorObject.targetAngle = rotatorObject.targetAngle + 5f;
                }
                canChangePressure = false;
            }
            else if (cd.outAngle >= cd.maxAngle - angleBoundsDelta)
            {
                canChangePressure = true;
            }
        }
    }
}
