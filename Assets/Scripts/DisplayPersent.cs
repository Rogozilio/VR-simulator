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
    public float persent;
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

        persent = (angle * 100f);
        Number.GetComponent<Text>().text = (Math.Round(persent, 2).ToString() + "%");
        //if (persent != float.Parse(Number.GetComponent<Text>().text))
        //{
        //    Number.GetComponent<Text>().text = /*(persent.ToString() + "%");*/(Math.Round(persent, 2).ToString() + "%");
        //}
    }
}
