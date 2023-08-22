using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class abilities : MonoBehaviour
{
	Movement move;
	UpdateRotation rot;
	[SerializeField]
	Rig rig;
	Controls controls;
	public bool isAiming;
	[SerializeField]
	public GameObject aimCast;
	AnimationStateController animCon;
	Animator anim;
	bool camSwitchCooldown = false;
    // Start is called before the first frame update
    void Start()
	{
		anim = GetComponentInChildren<Animator>();
		animCon = GetComponentInChildren<AnimationStateController>();
	    controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
	    move = GetComponent<Movement>();
	    rot = GetComponentInChildren<UpdateRotation>();
	    
    }
	
	void ResetFiring(){
		anim.SetBool("isFiring", false);
	}
	void resetCamSwitchCooldown(){
		camSwitchCooldown = false;
	}
    // Update is called once per frame
    void Update()
    {
	    if(Input.GetKey(controls.keys["aim"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
	    	rig.weight = 1f;
	    	rot.Aim();
	    	isAiming = true;
	    	aimCast.SetActive(true);
	    	
	    }
	    else if(!Input.GetKey(controls.keys["aim"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
		    rig.weight = 0f;
	    	rot.UnAim();
	    	isAiming = false;
	    	aimCast.SetActive(false);
	    	
	    }
	    if(isAiming){
	    	if(Input.GetKey(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
	    		anim.SetBool("isFiring", true);
	    		Invoke("ResetFiring", .1f);
	    	}
		    if(Input.GetKey(controls.keys["switchCam"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
		    	if(!camSwitchCooldown){
			    	rot.switchCam();
			    	camSwitchCooldown = true;
			    	Invoke("resetCamSwitchCooldown", 1f);
	    		}
	    	}
	    }
    }
}
