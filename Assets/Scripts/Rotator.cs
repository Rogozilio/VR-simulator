using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rotator : MonoBehaviour
{
    public PMeterArrowRotation[] Arrows;

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
