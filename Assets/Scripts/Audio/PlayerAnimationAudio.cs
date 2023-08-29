using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationAudio: MonoBehaviour
{

    private void PlayShootMissle()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerShootMissle, this.transform.position);
    }

    private void PlayFootstep()
    {
        
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerFootsteps, this.transform.position); 
    }

    private void PlayJump()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump, this.transform.position);
    }

    private void PlayDash()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDash, this.transform.position);
    }
}
