using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LinearLever : MonoBehaviour
{
    public Rotator rotatorObject;
    public LinearMapping linearMapping;
    public float directionCoef = 1f;
    private bool canChangePressure = true;
    private float boundsDelta = 0.1f;
    private float rotationAngle = 5f;

    void Update()
    {
        if (linearMapping.value <= 0f + boundsDelta)
        {
            canChangePressure = true;
        }
        else if (linearMapping.value >= 1f - boundsDelta)
        {
            if (canChangePressure)
            {
                rotatorObject.targetAngle = rotatorObject.targetAngle + rotationAngle * directionCoef;
            }
            canChangePressure = false;
        }
    }
}
