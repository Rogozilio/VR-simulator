using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SwitchAlign : MonoBehaviour
{
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
            if (inter.attachedToHand.IsGrabEnding(gameObject))
            {
                if (cd.outAngle < cd.minAngle + deltaAngle)
                {
                    cd.outAngle = cd.minAngle;
                }
                else if (cd.outAngle > cd.maxAngle - deltaAngle)
                {
                    cd.outAngle = cd.maxAngle;
                }
                else
                {
                    cd.outAngle = midAngle;
                }
            }
        }
    }
}
