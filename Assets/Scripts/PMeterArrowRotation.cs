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
    public bool canRotate = true;
    public float coef1 = 1f;
    public float coef2 = 1f;
    private bool areValuesSet = false;

    [SerializeField]
    private float currentPressure = 0f;
    public float CurrentPressure { get { return currentPressure; } }
    [SerializeField]
    private float targetPressure = 0f;
    public float TargetPressure { get { return targetPressure; } set { targetPressure = value; } }

    [SerializeField]
    private float minPressure = 0f;
    [SerializeField]
    private float maxPressure = 10f;
    private float finalTargetPressure = 0f;

    [SerializeField]
    private float minAngle = -60f;
    [SerializeField]
    private float maxAngle = 60f;
    [SerializeField]
    private float currentAngle = 0f;
    private float targetAngle = 0f;

    public float arrowAcceleration = 256f;

    void Start()
    {

    }

    void Update()
    {
        targetPressure = PS.StartPressureIN;

        if (canRotate)
        {
            UpdateAngleAndPressure();
            CheckPressure();
        }
    }

    private float ConvertPressureToAngle(float pressure)
    {
        if (isOUT)
        {
            pressure = pressure * coef1 * coef2 * 1000f / PS.CustomersNumber;
        }
        else
        {
            pressure = pressure * coef1 * coef2;
        }

        return (pressure - minPressure) / (maxPressure - minPressure) * (maxAngle - minAngle) + minAngle;
    }

    private float ConvertAngleToPressure(float angle)
    {
        return (angle - minAngle) / (maxAngle - minAngle) * (maxPressure - minPressure) + minPressure;
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
            currentAngle += Time.deltaTime * arrowAcceleration;// / 10f;
        }
        currentPressure = ConvertAngleToPressure(currentAngle);

        SetArrowAngle();
    }

    private void SetArrowAngle()
    {
        transform.localEulerAngles = new Vector3(0, 0, currentAngle);
    }

    private void CheckPressure()
    {
        float deltaP = 0.2f;
        if ((currentPressure >= finalTargetPressure - deltaP) && (currentPressure <= finalTargetPressure + deltaP))
        {
            canGoFurther = 1f;
            //Debug.Log("Ура! Всё работает!");
        }
        else
        {
            canGoFurther = 0f;
            //Debug.Log("Не всё работает, но это нормально");
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