using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rotator : MonoBehaviour
{
    public float targetAngle = 0;
    private float currentAngle = 0;
    [SerializeField]
    private float rotationSpeed = 10f;

    void FixedUpdate()
    {
        if (targetAngle > 90)
        {
            targetAngle = 90;
        }

        if (currentAngle < targetAngle)
        {
            currentAngle += Time.deltaTime * rotationSpeed;
        }

        transform.localEulerAngles = new Vector3(0, currentAngle, 0);
    }
}
