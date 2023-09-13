using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsAnimationAudio : MonoBehaviour
{
    private void PlayPlatformMovingAudio()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerServoSmall, this.transform.position);
    }
}
