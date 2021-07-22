using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PMeterArrowRotation : MonoBehaviour
{
    public int minPressure = 0;
    public int maxPressure = 100;
    [SerializeField]
    private int targetPressure = 0;
    [SerializeField]
    private int currentPressure = 0;

    public float arrowAcceleration = 256f;

    private float minAngle = -60f;
    private float maxAngle = 60f;
    //[SerializeField]
    private float targetAngle = 0f;
    //[SerializeField]
    private float currentAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        //if (targetPressure != currentPressure)
        //{
            UpdateAngleAndPressure();
        //}
    }

    public int GetCurrentPressure()
    {
        return currentPressure;
    }

    public void SetTargetPressure(int pressure)
    {
        targetPressure = pressure;
    }

    private float ConvertPressureToAngle(int pressure)
    {
        return (float) (pressure - minPressure) / (maxPressure - minPressure) * (maxAngle - minAngle) + minAngle; 
    }

    private int ConvertAngleToPressure(float angle)
    {
        return Convert.ToInt32((angle - minAngle) / (maxAngle - minAngle) * (maxPressure - minPressure) + minPressure);
    }

    private void UpdateAngleAndPressure()
    {
        targetAngle = ConvertPressureToAngle(targetPressure);
        if (currentAngle > targetAngle)
        {
            currentAngle -= Time.deltaTime * arrowAcceleration;
            //currentAngle = Math.Clamp(currentAngle, targetAngle, maxAngle);
        }
        else
        {
            currentAngle += Time.deltaTime * arrowAcceleration;
            //currentAngle = Math.Clamp(currentAngle, minAngle, targetAngle);
        }
        currentPressure = ConvertAngleToPressure(currentAngle);

        SetArrowAngle();
    }

    private void SetArrowAngle()
    {
        transform.localEulerAngles = new Vector3(0, 0, -currentAngle);
    }
}
