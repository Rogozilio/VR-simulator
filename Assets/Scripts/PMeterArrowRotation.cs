using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PMeterArrowRotation : MonoBehaviour
{
    private float canGoFurther = 0f;
    public float CanGoFurther { get { return canGoFurther; } }

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

    private float finalTargetPressure = 0f;

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
                finalTargetPressure = PS.TargetPressureOUT;
            }
            else
            {
                targetPressure = PS.StartPressureIN;
            }
            areValuesSet = true;
        }

        UpdateAngleAndPressure();
        CheckPressure();
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
            currentAngle += Time.deltaTime * arrowAcceleration / 10f;
        }
        currentPressure = ConvertAngleToPressure(currentAngle);

        SetArrowAngle();
    }

    private void SetArrowAngle()
    {
        transform.localEulerAngles = new Vector3(0, 0, -currentAngle);
    }

    private void CheckPressure()
    {
        float deltaP = 0.2f;
        if ((currentPressure >= finalTargetPressure - deltaP) && (currentPressure <= finalTargetPressure + deltaP))
        {
            canGoFurther = 1f;
            Debug.Log("Ура! Всё работает!");
        }
        else
        {
            canGoFurther = 0f;
            Debug.Log("Не всё работает, но это нормально");
        }
    }

    public void DoNothing()
    {
        Debug.Log("Подошли к Pressure");
    }

    public void DoEverything()
    {
        Debug.Log("Game Over");
    }
}
