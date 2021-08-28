using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioSource _audio;
    public AudioClip clip;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        _audio.PlayOneShot(clip);
    }

    public IEnumerator Corutina()
    {
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log(i++);
        }
    }
}
