﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("+ d'1 audio manager dans la scene");
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance (EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance (eventReference);
        return eventInstance;
    }
}
