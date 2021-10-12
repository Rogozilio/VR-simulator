using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LeverRotation : MonoBehaviour
{
    public Rotator rotatorObject;

    private CircularDrive cd;
    public float directionCoef = 1f;
    private bool canChangePressure = true;
    private float angleBoundsDelta = 2.5f;
    private float rotationAngle = 5f;

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
                canChangePressure = true;
            }
            else if (cd.outAngle >= cd.maxAngle - angleBoundsDelta)
            {
                if (canChangePressure)
                {
                    rotatorObject.targetAngle = rotatorObject.targetAngle + rotationAngle * directionCoef;
                }
                canChangePressure = false;
            }
        }
    }
}
