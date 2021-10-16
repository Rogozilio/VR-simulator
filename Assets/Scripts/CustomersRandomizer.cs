using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersRandomizer : MonoBehaviour
{
    public PressureStats PS;
    private float kostylEnding;
    public float KostylEnding { get { return kostylEnding; } }

    [SerializeField]
    private bool isActive;
    private float targetCustomersNumber;
    private float nextChangeDelta;
    private float nextChangeTimer;
    [SerializeField]
    private float totalTimer;
    private float changeSpeed;

    void Start()
    {
        kostylEnding = 0f;
        isActive = false;
        targetCustomersNumber = PS.CustomersNumber;
        nextChangeDelta = 60f;
        nextChangeTimer = nextChangeDelta;
        totalTimer = nextChangeDelta * 5f;
    }

    void Update()
    {
        if (isActive)
		{
            totalTimer -= Time.deltaTime;
            if (totalTimer < 0f)
            {
                HasEnded();
            }
            else
            {
                nextChangeTimer -= Time.deltaTime;
                if (nextChangeTimer < 0f)
                {
                    nextChangeTimer = 60f;
                    Randomize();
                }
                else
                {
                    PS.CustomersNumber += changeSpeed * Time.deltaTime;
                }
            }
        }
    }

    private void Randomize()
    {
        System.Random rnd = new System.Random();
        do
        {
            targetCustomersNumber = rnd.Next(100, 1000 + 1);
        } while ((targetCustomersNumber > PS.CustomersNumber - 200) && (targetCustomersNumber < PS.CustomersNumber + 200));
        changeSpeed = (targetCustomersNumber - PS.CustomersNumber) / nextChangeDelta;
    }

    private void HasEnded()
    {
        isActive = false;
        kostylEnding = 1f;
    }

    public void TurnOn()
    {
        if (!isActive)
        {
            Start();
            Randomize();
            isActive = true;
        }
    }
}
