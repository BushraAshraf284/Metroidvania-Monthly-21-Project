using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class PlayerAnimationAudio : MonoBehaviour
{
    private bool isFootstepSoundPlaying = false;
    public float footstepCooldown = 0.3f;
    private EventInstance playerFootsteps;
    private EventInstance playerDashStart;
    private EventInstance playerDashStop;
    private EventInstance playerDashJump;
    private EventInstance playerJump;
    private EventInstance playerJumpLand;
    private EventInstance playerFalling;
    private EventInstance playerJogJumpStart;
    private EventInstance playerJogJumpLand;

    private void PlayFootstep()
    {
        if (!isFootstepSoundPlaying)
        {
            isFootstepSoundPlaying = true;
            playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
            AudioManager.instance.PlaySoundFromAnimation(playerFootsteps);
        }
        Invoke(nameof(ResetFootstepSound), footstepCooldown);
    }

    private void ResetFootstepSound()
    {
        isFootstepSoundPlaying = false;
    }

    private void PlayShootMissle()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerShootMissle, this.transform.position);
    }

    private void PlayJump()
    {
        playerJump = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerJump);
        AudioManager.instance.PlaySoundFromAnimation(playerJump);
    }

    private void PlayJumpLand()
    {
        playerJumpLand = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerJumpLand);
        AudioManager.instance.PlaySoundFromAnimation(playerJumpLand);
    }

    private void PlayDashStart()
    {
        playerDashStart = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerDashStart);
        AudioManager.instance.PlaySoundFromAnimation(playerDashStart);
    }

    private void PlayDashStop()
    {
        playerDashStop = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerDashStop);
        AudioManager.instance.PlaySoundFromAnimation(playerDashStop);
    }

    private void PlayDashJump()
    {
        playerDashJump = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerDashJump);
        AudioManager.instance.PlaySoundFromAnimation(playerDashJump);
    }

    private void PlayFalling()
    {
        playerFalling = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFalling);
        AudioManager.instance.PlaySoundFromAnimation(playerFalling);
    }

    private void PlayJogJumpStart()
    {
        playerJogJumpStart = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerJogJumpStart);
        AudioManager.instance.PlaySoundFromAnimation(playerJogJumpStart);
    }
    
    private void PlayJogJumpLand()
    {
        playerJogJumpLand = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerJogJumpLand);
        AudioManager.instance.PlaySoundFromAnimation(playerJogJumpLand);
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
