using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ValveMinMaxAngleCheck : MonoBehaviour
{
    private bool isOpen = true;
    public bool IsOpen { get { return isOpen; } }
    private bool isClosed = false;
    public bool IsClosed { get { return isClosed; } }
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
                isOpen = false;
                isClosed = true;
            }
            else if (cd.outAngle >= cd.maxAngle - angleBoundsDelta)
            {
                isOpen = true;
                isClosed = false;
            }
            else
            {
                isOpen = false;
                isClosed = false;
            }
        }
        //Debug.Log("Angle: " + cd.outAngle);
        //Debug.Log("IsOpen: " + isOpen.ToString());
        //Debug.Log("IsClosed: " + isClosed.ToString());
    }
}
