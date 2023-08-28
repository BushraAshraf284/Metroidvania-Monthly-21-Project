using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class abilities : MonoBehaviour
{
	[SerializeField]
	GameObject shockProngHitbox;
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
	bool shockProngCooldown = false;
	[SerializeField]
	float shockProngdownCount = 1f;
	bool camSwitchCooldown = false;
	[SerializeField]
	float camSwitchCooldownCount = 1f;
	[SerializeField]
	float dashEnergyCost = 20f;
	public bool dashCooldown = false;
	[SerializeField]
	float dashCooldownCount = 1f;
	[SerializeField]
	float dashTimer = .1f;
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
	PlayerStats stats;
	[SerializeField]
	float shockSpikeDrainAmount = 50f;
    void Start()
	{
		stats = GetComponent<PlayerStats>();
		upgrades = GetComponent<UpgradeTracker>();
		anim = GetComponentInChildren<Animator>();
		animCon = GetComponentInChildren<AnimationStateController>();
	    controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
	    move = GetComponent<Movement>();
	    rot = GetComponentInChildren<UpdateRotation>();
	    
    }
	void resetDashing(){
		move.dashing = false;
	}
	public void SpawnShockProngHitbox(){
		shockProngHitbox.SetActive(true);
	}
	public void DeSpawnShockProngHitbox(){
		shockProngHitbox.SetActive(false);
	}
	public void Dash(){
		move.dashing = true;
		Invoke("resetDashing", dashTimer);
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
	void resetshockProngCooldown(){
		shockProngCooldown = false;
	}
	void resetDashCooldown(){
		dashCooldown = false;
	}

	
    // Update is called once per frame
    void Update()
    {
		//Handles the reloading for missiles
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
		//handles the reloading for spike
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
		//no aiming in reversed gravity
		//if(CustomGravity.GetGravity(rot.transform.position).y > 0){
		//	rig.weight = 0f;
	    //	rot.UnAim();
	    //	isAiming = false;
	   // 	aimCast.SetActive(false);
		//}

		//aiming
	    if(Input.GetKey(controls.keys["zoom"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && !move.delayedIsDashing){
			rot.Aim();
			isAiming = true;
			aimCast.SetActive(true);
			rig.weight = 1f;
	    }
		//un-aiming
	    else if((!Input.GetKey(controls.keys["zoom"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked) || move.delayedIsDashing){
		    rig.weight = 0f;
	    	rot.UnAim();
	    	isAiming = false;
	    	aimCast.SetActive(false);
	    	
	    }

	    if(isAiming){
			//shoot missiles
			if(upgrades.hasMissiles){
				if(Input.GetKey(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && !missileReloading){
					anim.SetBool("isFiring", true);
					Invoke("ResetFiring", .1f);

				}
			}
			//shoot shock spike
			if(upgrades.hasShockSpike){
				if(Input.GetKey(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && !spikeReloading){
					if(stats.charge - shockSpikeDrainAmount > 0){
						anim.SetBool("isFiring", true);
						Invoke("ResetFiring", .1f);
						stats.DrainBattery(shockSpikeDrainAmount);
					}
					else{
						Debug.Log("Not Enough Battery to use Shock Spike!");
					}
				}
			}
			//switch aiming shoulder
			if(Input.GetKey(controls.keys["switchCam"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked){
				if(!camSwitchCooldown){
					rot.switchCam();
					camSwitchCooldown = true;
					Invoke("resetCamSwitchCooldown", camSwitchCooldownCount);
				}
			}
	    }
		else{
			//Shock prong / sword
			if(Input.GetKeyDown(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && upgrades.hasShockProng){
				if(!shockProngCooldown){
					
					anim.SetBool("Pronging", true);
					anim.SetBool("Pronging2", true);
					shockProngCooldown = true;
					Invoke("resetshockProngCooldown", shockProngdownCount);

				}
			}
			if((Input.GetKeyUp(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused) || stats.charge <= 0){
				anim.SetBool("Pronging", false);
				anim.SetBool("Pronging2", false);
			}
			//Dash
			if(Input.GetKey(controls.keys["dash"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && animCon.movementPressed && move.OnGround && upgrades.hasJetBoost && !isAiming){
				//Debug.Log("Trying to dash!");
				if(!dashCooldown){
					if(stats.charge - dashEnergyCost < 0){
						Debug.Log("Not enough Battery!");
					}
					else{
						stats.DrainBattery(dashEnergyCost);
						anim.SetBool("Dash", true);
						dashCooldown = true;
						Invoke("resetDashCooldown", dashCooldownCount);
					}
				}
			}
		}

    }
}
