using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceDispatchers : MonoBehaviour
{
    public AudioSource _audio;

    // Start is called before the first frame update
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

}
