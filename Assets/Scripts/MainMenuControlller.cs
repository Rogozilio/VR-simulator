using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuControlller : MonoBehaviour
{
    private static float _gameMode = 0f;
    public GameObject Player;
    public GameObject mainButtons;
    public GameObject settingsObjects;
    public GameObject Bag;
    //public AudioSource Notification1;
    public AudioSource Noise;
    //public AudioSource Notification1T;
    //public AudioSource Notification2T;
    //public AudioSource Notification3T;
    //public AudioSource Notification4T;
    //public AudioSource Notification5T;
    //public bool IsTraining;
    public MainMenuControlller mainMenuControlller;
    public GameObject Planshet;
    public SCADA_Controller SCADA_Controller;
    public GameObject DialIN;
    public GameObject DialOUT;
    //public GameObject Valve1;
    //public GameObject Valve2;
    //public GameObject Valve3;
    //public GameObject Valve4;

    public static float GameMode => _gameMode;
    public float GameModeValue => _gameMode;

    private float State = 0f;

    public void LoadGameSceneExam()
    {
        //IsTraining = false;
        Player.transform.position = new Vector3(38.3f, 0.04f, -6f);
        Debug.Log("Игрок перемещён");
        //Notification1.Play();
        Noise.Play();
        _gameMode = 2f;
        //Bag.SetActive(true);
        //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void LoadGameSceneTraining()
    {
        //IsTraining = true;
        Player.transform.position = new Vector3(38.3f, 0.04f, -6f);
        Debug.Log("Игрок перемещён. Режим тренировки");
        //Notification1T.Play();
        Noise.Play();
        _gameMode = 1f;
        //AddOutline(Planshet);
        //AddOutlineVisible(DialIN);
        //AddOutlineVisible(DialOUT);
        //State = 1f;
        //SceneManager.LoadScene("TestingTeleportation", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        mainButtons.SetActive(false);
        settingsObjects.SetActive(true);
    }

    public void BackToMenu()
    {
        mainButtons.SetActive(true);
        settingsObjects.SetActive(false);
    }

    public void AddOutline(GameObject Object)
    {
        Object.AddComponent<Outline>();
        Object.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
        Object.GetComponent<Outline>().OutlineColor = Color.green;
        Object.GetComponent<Outline>().OutlineWidth = 3f;
    }

    public void AddOutlineVisible(GameObject Object)
    {
        Object.AddComponent<Outline>();
        Object.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
        Object.GetComponent<Outline>().OutlineColor = Color.green;
        Object.GetComponent<Outline>().OutlineWidth = 5f;
    }

    public void RemoveOutline(GameObject Object)
    {
        Destroy(Object.GetComponent<Outline>());
    }

    public void CheckState()
    {
        switch(State)
        {
            case 1f:
                //Notification2T.Play();
                break;
            case 2f:
                //Notification3T.Play();
                break;
            case 3f:
                //Notification4T.Play();
                break;
            case 4f:
                //Notification5T.Play();
                break;
            default:
                Debug.Log("Незафиксированное состояние");
                break;
        }
    }

    void FixedUpdate()
    {
        //if (mainMenuControlller.IsTraining == true)
        //{
        //    if (SCADA_Controller.PressuresTransferredCorrectly == 1f)
        //    {
        //        AddOutline(Valve1);
        //        RemoveOutline(Planshet);
        //        RemoveOutline(DialIN);
        //        RemoveOutline(DialOUT);
        //        State = 2f;
        //        //Notification2T.Play();
        //    }
        //    else State = 1f;

        //    if (Valve1.GetComponent<ValveMinMaxAngleCheck>().IsOpen == 1f)
        //    {
        //        AddOutline(Valve4);
        //        RemoveOutline(Valve1);
        //        State = 3f;
        //        //Notification3T.Play();
        //    }
        //    else if (Valve4.GetComponent<Outline>() != null)
        //    {
        //        RemoveOutline(Valve4);
        //    }

        //    if ((Valve1.GetComponent<ValveMinMaxAngleCheck>().IsOpen == 1f) && (Valve4.GetComponent<ValveMinMaxAngleCheck>().IsOpen == 1f))
        //    {
        //        AddOutlineVisible(Valve2);
        //        RemoveOutline(Valve4);
        //        State = 4f;
        //        //Notification4T.Play();
        //    }
        //    else if (Valve2.GetComponent<Outline>() != null)
        //    {
        //        RemoveOutline(Valve2);
        //    }

        //    if ((Valve1.GetComponent<ValveMinMaxAngleCheck>().IsOpen == 1f) && (Valve4.GetComponent<ValveMinMaxAngleCheck>().IsOpen == 1f) && (Valve2.GetComponent<ValveMinMaxAngleCheck>().IsClosed == 1f))
        //    {
        //        AddOutlineVisible(Valve3);
        //        RemoveOutline(Valve2);
        //        State = 5f;
        //        //Notification5T.Play();
        //    }
        //    else if (Valve3.GetComponent<Outline>() != null)
        //    {
        //        RemoveOutline(Valve3);
        //    }

        //    //CheckState();
        //}
    }
}
