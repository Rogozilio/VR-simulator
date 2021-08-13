using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PMeterArrowRotation : MonoBehaviour
{
    public PressureStats PS;
    public bool isOUT;

    private bool areValuesSet = false;
    private float minPressure = 0f;
    private float maxPressure = 25f;
    [SerializeField]
    private float currentPressure = 0f;
    public float CurrentPressure { get { return currentPressure; } }
    [SerializeField]
    private float targetPressure = 0f;
    public float TargetPressure { get { return targetPressure; } set { targetPressure = value; } }

    public float arrowAcceleration = 256f;

    private float minAngle = -60f;
    private float maxAngle = 60f;
    private float currentAngle = 0f;
    private float targetAngle = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (!areValuesSet)
		{
            if (isOUT)
            {
                targetPressure = PS.StartPressureOUT;
            }
            else
            {
                targetPressure = PS.StartPressureIN;
            }
            areValuesSet = true;
        }

        UpdateAngleAndPressure();
    }

    private float ConvertPressureToAngle(float pressure)
    {
        return (float) (pressure - minPressure) / (maxPressure - minPressure) * (maxAngle - minAngle) + minAngle; 
    }

    private float ConvertAngleToPressure(float angle)
    {
        return Convert.ToInt32((angle - minAngle) / (maxAngle - minAngle) * (maxPressure - minPressure) + minPressure);
    }

    private void UpdateAngleAndPressure()
    {
        targetAngle = ConvertPressureToAngle(targetPressure);
        if (currentAngle > targetAngle)
        {
            currentAngle -= Time.deltaTime * arrowAcceleration;
        }
        else
        {
            currentAngle += Time.deltaTime * arrowAcceleration;
        }
        currentPressure = ConvertAngleToPressure(currentAngle);

        SetArrowAngle();
    }

    private void SetArrowAngle()
    {
        transform.localEulerAngles = new Vector3(0, 0, -currentAngle);
    }
}
