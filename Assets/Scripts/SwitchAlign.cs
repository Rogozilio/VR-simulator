using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SwitchAlign : MonoBehaviour
{
    public LeverRotation Lever;
    private CircularDrive cd;
    private Interactable inter;
    private float midAngle;
    private float deltaAngle;

    void Start()
    {
        cd = gameObject.GetComponent(typeof(CircularDrive)) as CircularDrive;
        inter = gameObject.GetComponent(typeof(Interactable)) as Interactable;
        midAngle = (cd.maxAngle + cd.minAngle) / 2f;
        deltaAngle = (cd.maxAngle - cd.minAngle) / 4f;
    }

    void Update()
    {
        if ((cd.outAngle < cd.minAngle) || (cd.outAngle > cd.maxAngle))
        {
            // Error: angle out of bounds
        }
        else
        {
            /*Debug.Log("IsGrabEnding?");
            if (inter.attachedToHand.IsGrabEnding(gameObject))
            {
                Debug.Log("GrabEnded");
                if (cd.outAngle < cd.minAngle + deltaAngle)
                {
                    cd.outAngle = cd.minAngle;
                    Rot.directionCoef = -1f;
                }
                else if (cd.outAngle > cd.maxAngle - deltaAngle)
                {
                    cd.outAngle = cd.maxAngle;
                    Rot.directionCoef = 1f;
                }
                else
                {
                    cd.outAngle = midAngle;
                    Rot.directionCoef = 0f;
                }
            }*/
            if (cd.outAngle < cd.minAngle + deltaAngle)
            {
                Lever.directionCoef = 1f;
            }
            else if (cd.outAngle > cd.maxAngle - deltaAngle)
            {
                Lever.directionCoef = -1f;
            }
            else
            {
                Lever.directionCoef = 0f;
            }
        }
    }
}