using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ObjectsAnimationAudio : MonoBehaviour
{

    private StudioEventEmitter[] audioEmitters;

    private void Start()
    {
        audioEmitters = GetComponentsInChildren<FMODUnity.StudioEventEmitter>();


        if (audioEmitters == null || audioEmitters.Length == 0)
        {
            Debug.LogWarning("Audio Emitter not found on child GameObject.");
        }
    }

    public void PlaySound()
    {
        foreach (var emitter in audioEmitters)
        {
            if (emitter != null && !emitter.IsPlaying())
            {
                emitter.Play();
            }
        }
    }

    public void StopSound()
    {
        foreach (var emitter in audioEmitters)
        {
            if (emitter != null && emitter.IsPlaying())
            {
                emitter.Stop();
            }
        }
    }
}