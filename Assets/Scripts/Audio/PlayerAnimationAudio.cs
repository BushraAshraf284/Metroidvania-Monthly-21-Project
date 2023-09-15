using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerAnimationAudio : MonoBehaviour
{
    private bool isFootstepSoundPlaying = false;
    public float footstepCooldown = 0.3f;
    private EventInstance fallingSound;
    private EventInstance shockSound;

    private void Start()
    {
        fallingSound = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFalling);
        shockSound = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerShockProngMid);
    }

    private EventInstance PlayEvent(EventReference eventReference)
    {
        EventInstance eventInstance = AudioManager.instance.CreateEventInstance(eventReference);
        AudioManager.instance.PlaySoundFromAnimation(eventInstance);
        return eventInstance;
    }

    private void PlayFootstep()
    {
        if (!isFootstepSoundPlaying)
        {
            isFootstepSoundPlaying = true;
            PlayEvent(FMODEvents.instance.playerFootsteps);
        }
        Invoke(nameof(ResetFootstepSound), footstepCooldown);
    }

    private void ResetFootstepSound()
    {
        isFootstepSoundPlaying = false;
    }

    private void PlayFallingSound()
    {
        fallingSound.start();
    }

    private void StopFallingSound()
    {
        fallingSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void PlayShootMissle()
    {
        PlayEvent(FMODEvents.instance.playerShootMissle);
    }

    private void PlayShockProngStart()
    {
        shockSound.start();
        Debug.Log("Starting shock sound");
    }

    private void PlayShockProngMid()
    {
        // 
    }

    private void PlayShockProngEnd()
    {
        shockSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        Debug.Log("Ending shock sound");
    }

    private void PlaySwordAttack()
    {
        PlayEvent(FMODEvents.instance.playerSwordAttack);
    }

    private void PlaySwordAttackTwo()
    {
        PlayEvent(FMODEvents.instance.playerSwordAttackTwo);
    }

    private void PlaySwordAttackThree()
    {
        PlayEvent(FMODEvents.instance.playerSwordAttackThree);
    }

    private void PlayJump()
    {
        PlayEvent(FMODEvents.instance.playerJump);
    }

    private void PlayJumpLand()
    {
        PlayEvent(FMODEvents.instance.playerJumpLand);
    }

    private void PlayDashStart()
    {
        PlayEvent(FMODEvents.instance.playerDashStart);
    }

    private void PlayDashStop()
    {
        PlayEvent(FMODEvents.instance.playerDashStop);
    }

    private void PlayDashJump()
    {
        PlayEvent(FMODEvents.instance.playerDashJump);
    }

    private void PlayJogJumpStart()
    {
        PlayEvent(FMODEvents.instance.playerJogJumpStart);
    }

    private void PlayJogJumpLand()
    {
        PlayEvent(FMODEvents.instance.playerJogJumpLand);
    }

    private void PlayVerticalBoost()
    {
        PlayEvent(FMODEvents.instance.playerVerticalBoost);
    }

    private void PlayServoSmall()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerServoSmall, this.transform.position);
    }

    private void PlayServoMedium()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerServoMedium, this.transform.position);
    }

    private void PlayServoHeavy()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerServoHeavy, this.transform.position);
    }

    private void PlayServoCranking()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerServoCranking, this.transform.position);
    }
}
