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
	GameObject spike;
	[SerializeField]
	GameObject spikeShell;
	[SerializeField]
	Transform missileSpawnPos;
	[SerializeField]
	Transform spikeshellSpawnPos;
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
	GameObject spikePrefab;
	GameObject spikeShellPrefab;
	public UpgradeTracker upgrades;
	[SerializeField]
	float missileReloadTime = 3f;
	float missileReloadCount;
	[SerializeField]
	float SpikeReloadTime = 5f;
	float SpikeReloadCount;
	[SerializeField]
	GameObject worldMissile;
	public bool missileReloading;
	public bool spikeReloading;
	[SerializeField]
	GameObject worldSpike;
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
			missileReloading = true;
			missileReloadCount = 0f;
			worldMissile.SetActive(false);
		}
	}
	public void fireSpike(){
		spikePrefab = Instantiate(spike, missileSpawnPos.position, Quaternion.LookRotation(( missileSpawnPos.position - aimCast.transform.position), CustomGravity.GetUpAxis(this.transform.position)));
		spikeShellPrefab = Instantiate(spikeShell, spikeshellSpawnPos.position, Quaternion.LookRotation(( aimCast.transform.position - missileSpawnPos.position ), CustomGravity.GetUpAxis(this.transform.position)));
		if(spikePrefab.GetComponent<Thruster>() != null){
			spikePrefab.GetComponent<Thruster>().StartBurstForce(aimCast.transform);
			if(spikeShellPrefab.GetComponent<Rigidbody>() != null){
				spikeShellPrefab.GetComponent<Rigidbody>().velocity = ((spikeshellSpawnPos.position - aimCast.transform.position).normalized * 5f + CustomGravity.GetUpAxis(this.transform.position) * 5f);
			}
			spikeReloading = true;
			SpikeReloadCount = 0f;
			worldSpike.SetActive(false);
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
		if(missileReloading){
			if(missileReloadCount < missileReloadTime){
				missileReloadCount += Time.deltaTime;
			}
			else{
				worldMissile.SetActive(true);
				missileReloading = false;
				missileReloadCount = 0f;
			}
		}
		if(spikeReloading){
			if(SpikeReloadCount < SpikeReloadTime){
				SpikeReloadCount += Time.deltaTime;
			}
			else{
				worldSpike.SetActive(true);
				spikeReloading = false;
				SpikeReloadCount = 0f;
			}
		}
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
			if(upgrades.hasMissiles){
				if(Input.GetKey(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && !missileReloading){
					anim.SetBool("isFiring", true);
					Invoke("ResetFiring", .1f);

				}
			}
			if(upgrades.hasShockSpike){
				if(Input.GetKey(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && !spikeReloading){
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
