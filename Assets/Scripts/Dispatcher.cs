using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    private byte index; 
    private AudioSource _audio;
    public AudioClip[] Clips;
    public AudioClip[] ClipsT;
    //public AudioClip Notification1;
    //public AudioClip Notification1T;
    //public AudioClip Notification2T;
    //public AudioClip Notification3T;
    //public AudioClip Notification4T;
    //public AudioClip Notification5T;
    ////public AudioClip Notification1T;

    // Start is called before the first frame update
    void Awake()
    {
        index = 0;
        _audio = GetComponent<AudioSource>();
    }
    //public void PlayName1()
    //{
    //    _audio.PlayOneShot(Notification1);
    //}
    //public void PlayName1T()
    //{
    //    _audio.PlayOneShot(Notification1T);
    //}
    //public void PlayName2T()
    //{
    //    _audio.PlayOneShot(Notification2T);
    //}
    //public void PlayName3T()
    //{
    //    _audio.PlayOneShot(Notification3T);
    //}
    //public void PlayName4T()
    //{
    //    _audio.PlayOneShot(Notification4T);
    //}
    //public void PlayName5T()
    //{
    //    _audio.PlayOneShot(Notification5T);
    //}

    public void Play()
    {
        if (MainMenuControlller.GameMode == 1)
        {
            if (index < ClipsT.Length)
            {
                _audio.PlayOneShot(ClipsT[index]);
                index++;
            }
        }
        else if (MainMenuControlller.GameMode == 2)
        {
            if (index < Clips.Length)
            {
                _audio.PlayOneShot(Clips[index]);
                index++;
            }
        }
    }
}
