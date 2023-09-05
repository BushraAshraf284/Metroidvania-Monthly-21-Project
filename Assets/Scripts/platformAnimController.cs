using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;

public class platformAnimController : MonoBehaviour
{
	public bool isOpened;
    Animator anim;
    // Start is called before the first frame update
    public bool isActivated;
    //soundeffect sound

    // public void startSound(){
        //Sound.play;
    //}

    // public void stop sound(){
        //Sound.stop;
    //}
    void Start()
    {
	    anim = GetComponent<Animator>();
	    if(isOpened){
		    anim.SetBool("Activated", true);
		    isActivated = true;
	    }
    }
    public void ResetActivated(){
        anim.SetBool("Activated", false);
        isActivated = false;
    }
    public void ResetExtraActivated(){
        anim.SetBool("ExtraActivation", false);
        isActivated = false;
    }
    public void Activated(){
        anim.SetBool("Activated", true);
	    isActivated = true;
	    isOpened = true;
    }
    public void ExtraActivated(){
        anim.SetBool("v", true);
	    isActivated = true;
	    isOpened = true;
    }
    public void TempActivation(float time){
        isActivated = true;
        anim.SetBool("Activated", true);
        Invoke("ResetActivated", time);
        
    }
     public bool AnimatorIsPlaying(){
    return anim.GetCurrentAnimatorStateInfo(0).length >
           anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

}
