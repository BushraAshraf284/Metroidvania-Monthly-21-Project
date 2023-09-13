using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ObjectsAnimationAudio : MonoBehaviour
{

    private StudioEventEmitter audioEmitter;

    private void Start()
    {
        audioEmitter = GetComponentInChildren<FMODUnity.StudioEventEmitter>();


        if (audioEmitter == null)
        {
            Debug.LogWarning("Audio Emitter not found on child GameObject.");
        }
    }

    public void PlaySound()
    {
        if (audioEmitter != null && !audioEmitter.IsPlaying())
        {
            audioEmitter.Play();
        }
    }

    public void StopSound()
    {
        if (audioEmitter != null && audioEmitter.IsPlaying())
        {
            audioEmitter.Stop();
        }
    }
}