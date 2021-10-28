using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SCADA_Controller : MonoBehaviour
{
    private float pressuresTransferredCorrectly = 0f;
    public float PressuresTransferredCorrectly { get { return pressuresTransferredCorrectly; } }

    public PressureStats PS;
    //public PMeterArrowRotation AR;
    public bool onHand = false;
    private Quaternion oldRotation;

    public GameObject scada_image;
    public GameObject transferObjects;
    //public GameObject settingsObjects;
    //public GameObject player;
    public GameObject InNumber;
    public GameObject OutNumber;
    public GameObject ReqField;

    public GameObject Bag;
    public bag bag;

    public AudioClip Notification1;
    public AudioClip Notification1T;

    public void SetTransferObjects()
    {
        scada_image.SetActive(false);
        transferObjects.SetActive(true);
        //settingsObjects.SetActive(false);
    }

    public void SetScadaImage()
    {
        scada_image.SetActive(true);
        transferObjects.SetActive(false);
        //settingsObjects.SetActive(false);
    }

    /*public void SetSettingsObjects()
    {
        scada_image.SetActive(false);
        transferObjects.SetActive(false);
        settingsObjects.SetActive(true);
    }*/

    /*private void Update()
    {
        if(!onHand)
        {
            transform.position = player.transform.position + new Vector3(0, 0.5f, -0.5f);
        }
    }*/

    public void SetOnHand()
    {
        onHand = true;
        GetComponent<Rigidbody>().isKinematic = false;
        //transform.parent = null;
        bag.OnBag = false;
    }

    public void DetachFromHand()
    {
        onHand = false;
        
        //GetComponent<Rigidbody>().isKinematic = true;

        //transform.position = player.transform.position + new Vector3(0, 1.0f, 0.5f);
        //transform.parent = player.transform;
        //oldRotation = transform.rotation;
        //transform.rotation = Quaternion.Euler(oldRotation.x, oldRotation.y, 0);
    }

    public void UpInNumber()
    {
        InNumber.GetComponent<Text>().text = (Int32.Parse(InNumber.GetComponent<Text>().text) + 1).ToString();
    }

    public void DownInNumber()
    {
        InNumber.GetComponent<Text>().text = (Int32.Parse(InNumber.GetComponent<Text>().text) - 1).ToString();
    }

    public void UpOutNumber()
    {
        OutNumber.GetComponent<Text>().text = (Int32.Parse(OutNumber.GetComponent<Text>().text) + 1).ToString();
    }

    public void DownOutNumber()
    {
        OutNumber.GetComponent<Text>().text = (Int32.Parse(OutNumber.GetComponent<Text>().text) - 1).ToString();
    }

    public void Transfer()
    {
        //if ((Single.Parse(InNumber.GetComponent<Text>().text) == PS.StartPressureIN) &&
        //    (Single.Parse(OutNumber.GetComponent<Text>().text) == PS.StartPressureOUT))
        //{
        //    ReqField.GetComponent<Text>().text = PS.TargetPressureOUT.ToString();
        //    pressuresTransferredCorrectly = 1f;
        //}
        //else
        //{
            pressuresTransferredCorrectly = 0f;
            ReqField.GetComponent<Text>().text = "Показания не соответсвуют ГОСТу";
        //}
    }

    void Update()
    {
        if (bag.OnBag)
        {
            transform.position = Bag.transform.position;
            transform.localEulerAngles = new Vector3(Bag.transform.localEulerAngles.x + 180, Bag.transform.localEulerAngles.y + 180, Bag.transform.localEulerAngles.z + 90);
        }
    }
    public void PlayDispatcher()
    {
        Dispatcher dispatcher = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Dispatcher>();
        dispatcher.Play();
    }
}
