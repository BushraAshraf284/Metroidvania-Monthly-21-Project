using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class abilities : MonoBehaviour
{
	[SerializeField]
	GameObject missile;
	[SerializeField]
	Transform missileSpawnPos;
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
	[SerializeField]
	float camSwitchCooldownCount = 1f;
	[SerializeField]
	GameObject forearm;
    // Start is called before the first frame update
	GameObject missilePrefab;
	public UpgradeTracker upgrades;
    void Start()
	{
		upgrades = GetComponent<UpgradeTracker>();
		anim = GetComponentInChildren<Animator>();
		animCon = GetComponentInChildren<AnimationStateController>();
	    controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
	    move = GetComponent<Movement>();
	    rot = GetComponentInChildren<UpdateRotation>();
	    
    }
	public void fireMissile(){
		missilePrefab = Instantiate(missile, missileSpawnPos.position, Quaternion.LookRotation(( missileSpawnPos.position - aimCast.transform.position), CustomGravity.GetUpAxis(this.transform.position)));
		if(missilePrefab.GetComponent<Thruster>() != null){
			missilePrefab.GetComponent<Thruster>().StartForce(aimCast.transform);
		}
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
	    if(Input.GetKey(controls.keys["zoom"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
	    	rot.Aim();
	    	isAiming = true;
	    	aimCast.SetActive(true);
			rig.weight = 1f;
	    	
	    }
	    else if(!Input.GetKey(controls.keys["zoom"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
		    rig.weight = 0f;
	    	rot.UnAim();
	    	isAiming = false;
	    	aimCast.SetActive(false);
	    	
	    }
	    if(isAiming){
			if(upgrades.hasMissiles || upgrades.hasShockSpike){
				if(Input.GetKey(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
					anim.SetBool("isFiring", true);
					Invoke("ResetFiring", .1f);

				}
			}
			if(Input.GetKey(controls.keys["switchCam"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
				if(!camSwitchCooldown){
					rot.switchCam();
					camSwitchCooldown = true;
					Invoke("resetCamSwitchCooldown", camSwitchCooldownCount);
				}
			}
			
	    }
    }
}
