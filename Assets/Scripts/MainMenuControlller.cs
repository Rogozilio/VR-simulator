using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuControlller : MonoBehaviour
{
    public GameObject Player;
    public GameObject mainButtons;
    public GameObject settingsObjects;

    public void LoadGameSceneExam()
    {
        Player.transform.position = new Vector3(10, 0, -8);
        //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void LoadGameSceneTraining()
    {
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


}
