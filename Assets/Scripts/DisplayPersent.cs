using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class DisplayPersent : MonoBehaviour
{
    public GameObject Number;
    public PMeterArrowRotation[] Arrows;
    private CircularDrive cd;
    private int persent;
    [SerializeField]
    private float angle;

    void Start()
    {
        cd = gameObject.GetComponent(typeof(CircularDrive)) as CircularDrive;
    }

    // Update is called once per frame
    void Update()
    {
        angle = cd.outAngle / (cd.maxAngle - cd.minAngle);
        foreach (PMeterArrowRotation arr in Arrows)
        {
            arr.coef2 = angle;
        }

        persent = (int)(angle * 100f);
        if (persent != Int32.Parse(Number.GetComponent<Text>().text))
        {
            Number.GetComponent<Text>().text = persent.ToString();
        }
    }
}
