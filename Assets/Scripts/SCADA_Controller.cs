using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.IO;
using UnityEngine.SceneManagement;

public class SCADA_Controller : MonoBehaviour
{
    public GameObject scada_image;
    public GameObject transferObjects;
    public GameObject settingsObjects;

    public void SetTransferObjects()
    {
        scada_image.SetActive(false);
        transferObjects.SetActive(true);
        settingsObjects.SetActive(false);
    }

    public void SetScadaImage()
    {
        scada_image.SetActive(true);
        transferObjects.SetActive(false);
        settingsObjects.SetActive(false);
    }

    public void SetSettingsObjects()
    {
        scada_image.SetActive(false);
        transferObjects.SetActive(false);
        settingsObjects.SetActive(true);
    }
}
