using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ValveMinMaxAngleCheck : MonoBehaviour
{
    private float isOpen = 1f;
    public float IsOpen { get { return isOpen; } }
    private float isClosed = 0f;
    public float IsClosed { get { return isClosed; } }
    private float angleBoundsDelta = 5f;
    private CircularDrive cd;

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
                isOpen = 0f;
                isClosed = 1f;
            }
            else if (cd.outAngle >= cd.maxAngle - angleBoundsDelta)
            {
                isOpen = 1f;
                isClosed = 0f;
            }
            else
            {
                isOpen = 0f;
                isClosed = 0f;
            }
        }
        //Debug.Log("Angle: " + cd.outAngle);
        //Debug.Log("IsOpen: " + isOpen.ToString());
        //Debug.Log("IsClosed: " + isClosed.ToString());
    }

    public void DoNothing()
    {
        Debug.Log("Подошли к Valve");
    }
}
