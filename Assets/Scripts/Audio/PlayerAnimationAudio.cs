using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationAudio : MonoBehaviour
{
    private bool isFootstepSoundPlaying = false;
    public float footstepCooldown = 0.3f;

    private void PlayFootstep()
    {
        if (!isFootstepSoundPlaying)
        {
            isFootstepSoundPlaying = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerFootsteps, this.transform.position);
        }
        Invoke(nameof(ResetFootstepSound), footstepCooldown);
    }

    private void ResetFootstepSound()
    {
        isFootstepSoundPlaying = false;
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

    private void PlayShootMissle()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerShootMissle, this.transform.position);
    }

    private void PlayJump()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump, this.transform.position);
    }

    private void PlayJumpLand()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJumpLand, this.transform.position);
    }

    private void PlayDashStart()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDashStart, this.transform.position);
    }

    private void PlayDashStop()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDashStop, this.transform.position);
    }

    private void PlayDashJump()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDashJump, this.transform.position);
    }

    private void PlayFalling()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerFalling, this.transform.position);
    }

    private void PlayJogJumpStart()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJogJumpStart, this.transform.position);
    }
    
    private void PlayJogJumpLand()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJogJumpLand, this.transform.position);
    }
}
