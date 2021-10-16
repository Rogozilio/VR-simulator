using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rotator : MonoBehaviour
{
    public PMeterArrowRotation[] Arrows;
    private float kostylOpen;
    public float KostylOpen { get { return kostylOpen; } }
    private float kostylClosed;
    public float KostylClosed { get { return kostylClosed; } }

    public float targetAngle = 0f;
    private float minAngle = 0f;
    private float maxAngle = 90f;
    private float currentAngle = 0f;
    [SerializeField]
    private float rotationSpeed = 10f;

    void FixedUpdate()
    {
        if (targetAngle > maxAngle)
        {
            targetAngle = maxAngle;
        }

        if (targetAngle < minAngle)
        {
            targetAngle = minAngle;
        }

        if ((currentAngle > maxAngle - 1f) && (currentAngle < maxAngle + 1f))
        {
            kostylOpen = 1f;
        }
        else if ((currentAngle > minAngle - 1f) && (currentAngle < minAngle + 1f))
        {
            kostylClosed = 1f;
        }
        else
        {
            kostylOpen = 0f;
            kostylClosed = 0f;
        }

        if (currentAngle < targetAngle)
        {
            currentAngle += Time.deltaTime * rotationSpeed;
        }
        else if (currentAngle > targetAngle)
        {
            currentAngle -= Time.deltaTime * rotationSpeed;
        }

        foreach (PMeterArrowRotation arr in Arrows)
        {
            arr.coef1 = currentAngle / maxAngle;
        }

        transform.localEulerAngles = new Vector3(0, -currentAngle, 0);
    }
}
