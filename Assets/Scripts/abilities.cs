using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class abilities : MonoBehaviour
{
	[SerializeField]
	GameObject missile1, missile2, missile3, missile4, missile5, missile6;
	[SerializeField]
	GameObject homingMissilePrefab;
	[SerializeField]
	Transform homingMissileSpawnPosition1, homingMissileSpawnPosition2, homingMissileSpawnPosition3, homingMissileSpawnPosition4, homingMissileSpawnPosition5, homingMissileSpawnPosition6;
	[SerializeField]
	GameObject homingMissileVolume;
	[SerializeField]
	GameObject aimingCrosshair, notAimingCrosshair;
	public bool canFollowUpGate;
	public bool swordAttacking;
	[SerializeField]
	public GameObject swordHitbox;
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
	GameObject homingMissilePlaceholder;
	GameObject spikePrefab;
	GameObject spikeShellPrefab;
	[HideInInspector]
	public UpgradeTracker upgrades;
	[SerializeField]
	float homingMissileReloadTime = 10f;
	float homingMissileReloadCount;
	[SerializeField]
	float missileReloadTime = 3f;
	float missileReloadCount;
	[SerializeField]
	float SpikeReloadTime = 5f;
	float SpikeReloadCount;
	[SerializeField]
	GameObject worldMissile;
	public bool homingMissileReloading;
	public bool missileReloading;
	public bool spikeReloading;
	[SerializeField]
	GameObject worldSpike;
	PlayerStats stats;
	[SerializeField]
	float shockSpikeDrainAmount = 50f;
	[SerializeField]
	[Tooltip("How long between each missile barrage")]
	float homingMissileSpacingCap = .5f;
	float homingMissileSpacingCount;
	bool homingMissileSpacingGate = true;
	bool homingMissileSpacingGate2 = true;
	[SerializeField]
	float homingMissileUpwardForceMax;
	[SerializeField]
	float homingMissileUpwardForceMin;
	float homingMissileUpwardForce;

	Transform homingMissileTarget1, homingMissileTarget2, homingMissileTarget3;

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
	public void SetCanFollowUpGateFalse(){
		canFollowUpGate = false;
		anim.SetBool("CanFollowUp", false);
	}
	public void SetCanFollowUpGateTrue(){
		canFollowUpGate = true;
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
	void FireMissile1(Transform target){
		Debug.Log("Firig missile #1 at " + target);
		homingMissileUpwardForce = UnityEngine.Random.Range(homingMissileUpwardForceMin, homingMissileUpwardForceMax);
		homingMissilePlaceholder = Instantiate(homingMissilePrefab, homingMissileSpawnPosition1.position, Quaternion.LookRotation(( homingMissileSpawnPosition1.position - aimCast.transform.position), CustomGravity.GetUpAxis(this.transform.position)));
		if(homingMissilePlaceholder.GetComponent<Thruster>() != null){
			homingMissilePlaceholder.GetComponent<Rigidbody>().AddForce(this.transform.up * homingMissileUpwardForce);
			homingMissilePlaceholder.GetComponent<Thruster>().StartForce(target);
			missile1.SetActive(false);
		}
	}
	void FireMissile2(Transform target){
		Debug.Log("Firig missile #2 at " + target);
		homingMissileUpwardForce = UnityEngine.Random.Range(homingMissileUpwardForceMin, homingMissileUpwardForceMax);
		homingMissilePlaceholder = Instantiate(homingMissilePrefab, homingMissileSpawnPosition2.position, Quaternion.LookRotation(( homingMissileSpawnPosition2.position - aimCast.transform.position), CustomGravity.GetUpAxis(this.transform.position)));
		if(homingMissilePlaceholder.GetComponent<Thruster>() != null){
			homingMissilePlaceholder.GetComponent<Rigidbody>().AddForce(this.transform.up * homingMissileUpwardForce);
			homingMissilePlaceholder.GetComponent<Thruster>().StartForce(target);
			missile2.SetActive(false);
		}
	}
	void FireMissile3(Transform target){
		Debug.Log("Firig missile #3 at " + target);
		homingMissileUpwardForce = UnityEngine.Random.Range(homingMissileUpwardForceMin, homingMissileUpwardForceMax);
		homingMissilePlaceholder = Instantiate(homingMissilePrefab, homingMissileSpawnPosition3.position, Quaternion.LookRotation(( homingMissileSpawnPosition3.position - aimCast.transform.position), CustomGravity.GetUpAxis(this.transform.position)));
		if(homingMissilePlaceholder.GetComponent<Thruster>() != null){
			homingMissilePlaceholder.GetComponent<Rigidbody>().AddForce(this.transform.up * homingMissileUpwardForce);
			homingMissilePlaceholder.GetComponent<Thruster>().StartForce(target);
			missile3.SetActive(false);
		}
	}
	void FireMissile4(Transform target){
		Debug.Log("Firig missile #4 at " + target);
		homingMissileUpwardForce = UnityEngine.Random.Range(homingMissileUpwardForceMin, homingMissileUpwardForceMax);
		homingMissilePlaceholder = Instantiate(homingMissilePrefab, homingMissileSpawnPosition4.position, Quaternion.LookRotation(( homingMissileSpawnPosition4.position - aimCast.transform.position), CustomGravity.GetUpAxis(this.transform.position)));
		if(homingMissilePlaceholder.GetComponent<Thruster>() != null){
			homingMissilePlaceholder.GetComponent<Rigidbody>().AddForce(this.transform.up * homingMissileUpwardForce);
			homingMissilePlaceholder.GetComponent<Thruster>().StartForce(target);
			missile4.SetActive(false);
		}
	}
	void FireMissile5(Transform target){
		Debug.Log("Firig missile #5 at " + target);
		homingMissileUpwardForce = UnityEngine.Random.Range(homingMissileUpwardForceMin, homingMissileUpwardForceMax);
		homingMissilePlaceholder = Instantiate(homingMissilePrefab, homingMissileSpawnPosition5.position, Quaternion.LookRotation(( homingMissileSpawnPosition5.position - aimCast.transform.position), CustomGravity.GetUpAxis(this.transform.position)));
		if(homingMissilePlaceholder.GetComponent<Thruster>() != null){
			homingMissilePlaceholder.GetComponent<Rigidbody>().AddForce(this.transform.up * homingMissileUpwardForce);
			homingMissilePlaceholder.GetComponent<Thruster>().StartForce(target);
			missile5.SetActive(false);
		}
	}
	void FireMissile6(Transform target){
		Debug.Log("Firig missile #6 at " + target);
		homingMissileUpwardForce = UnityEngine.Random.Range(homingMissileUpwardForceMin, homingMissileUpwardForceMax);
		homingMissilePlaceholder = Instantiate(homingMissilePrefab, homingMissileSpawnPosition6.position, Quaternion.LookRotation(( homingMissileSpawnPosition6.position - aimCast.transform.position), CustomGravity.GetUpAxis(this.transform.position)));
		if(homingMissilePlaceholder.GetComponent<Thruster>() != null){
			homingMissilePlaceholder.GetComponent<Rigidbody>().AddForce(this.transform.up * homingMissileUpwardForce);
			homingMissilePlaceholder.GetComponent<Thruster>().StartForce(target);
			missile6.SetActive(false);
		}
		homingMissileReloading = true;
		homingMissileReloadCount = 0f;
	}

	
    // Update is called once per frame
    void Update()
    {
		if(!homingMissileSpacingGate){
			if(homingMissileSpacingCount < homingMissileSpacingCap){
				homingMissileSpacingCount += Time.deltaTime;
			}
			else{
				homingMissileSpacingCount = 0f;
				homingMissileSpacingGate = true;
				if(homingMissileTarget2 != null){
					FireMissile3(homingMissileTarget2);
					FireMissile4(homingMissileTarget2);
					homingMissileSpacingGate2 = false;
				}
			}
		}
		if(!homingMissileSpacingGate2){
			if(homingMissileSpacingCount < homingMissileSpacingCap){
				homingMissileSpacingCount += Time.deltaTime;
			}
			else{
				homingMissileSpacingCount = 0f;
				homingMissileSpacingGate2 = true;
				if(homingMissileTarget3 != null){
					FireMissile5(homingMissileTarget3);
					FireMissile6(homingMissileTarget3);

				}
			}
		}
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
		if(homingMissileReloading){
			if(homingMissileReloadCount < homingMissileReloadTime){
				homingMissileReloadCount += Time.deltaTime;
			}
			else{
				missile1.SetActive(true);
				missile2.SetActive(true);
				missile3.SetActive(true);
				missile4.SetActive(true);
				missile5.SetActive(true);
				missile6.SetActive(true);
				homingMissileReloading = false;
				homingMissileReloadCount = 0f;
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
	    if(Input.GetKey(controls.keys["zoom"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && !move.delayedIsDashing && !swordAttacking){
			aimingCrosshair.SetActive(true);
			notAimingCrosshair.SetActive(false);
			rot.Aim();
			isAiming = true;
			aimCast.SetActive(true);
			rig.weight = 1f;
			if(upgrades.hasHomingMissiles){
				homingMissileVolume.SetActive(true);
			}
			
	    }
		//un-aiming
	    else if((!Input.GetKey(controls.keys["zoom"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked) || move.delayedIsDashing || swordAttacking){
			homingMissileSpacingGate = true;
			homingMissileSpacingGate2 = true;
			homingMissileSpacingCount = 0f;
			aimingCrosshair.SetActive(false);
			notAimingCrosshair.SetActive(true);
		    rig.weight = 0f;
	    	rot.UnAim();
	    	isAiming = false;
	    	aimCast.SetActive(false);
			homingMissileVolume.SetActive(false);
			homingMissileVolume.GetComponent<HomingMissileTracking>().ClearList();
	    	
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
			if(upgrades.hasHomingMissiles){
				if(Input.GetKeyDown(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && !homingMissileReloading){
					if(homingMissileVolume.GetComponent<HomingMissileTracking>().target1 != null){
						
						if(homingMissileVolume.GetComponent<HomingMissileTracking>().target2 != null){
							
							if(homingMissileVolume.GetComponent<HomingMissileTracking>().target3 != null){
								//all three targets locked
								homingMissileReloading = true;
								homingMissileTarget1 = homingMissileVolume.GetComponent<HomingMissileTracking>().target1.transform;
								homingMissileTarget2 = homingMissileVolume.GetComponent<HomingMissileTracking>().target2.transform;
								homingMissileTarget3 = homingMissileVolume.GetComponent<HomingMissileTracking>().target3.transform;

								FireMissile1(homingMissileTarget1);
								FireMissile2(homingMissileTarget1);
								homingMissileSpacingGate = false;
								
							}	
							else{
								//only two tracked targets
								homingMissileReloading = true;
								homingMissileTarget1 = homingMissileVolume.GetComponent<HomingMissileTracking>().target1.transform;
								homingMissileTarget2 = homingMissileVolume.GetComponent<HomingMissileTracking>().target2.transform;
								homingMissileTarget3 = homingMissileVolume.GetComponent<HomingMissileTracking>().target2.transform;

								FireMissile1(homingMissileTarget1);
								FireMissile2(homingMissileTarget1);
								homingMissileSpacingGate = false;
							}
						}
						else{
							homingMissileReloading = true;
							homingMissileTarget1 = homingMissileVolume.GetComponent<HomingMissileTracking>().target1.transform;
							homingMissileTarget2 = homingMissileVolume.GetComponent<HomingMissileTracking>().target1.transform;
							homingMissileTarget3 = homingMissileVolume.GetComponent<HomingMissileTracking>().target1.transform;

							FireMissile1(homingMissileTarget1);
							FireMissile2(homingMissileTarget1);
							homingMissileSpacingGate = false;
							//only one tracked target
						}
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
			if((Input.GetKeyUp(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused) || stats.charge <= 0 && upgrades.hasShockProng){
				anim.SetBool("Pronging", false);
				anim.SetBool("Pronging2", false);
			}
			if(Input.GetKeyDown(controls.keys["attack"]) && !FindObjectOfType<PauseMenu>().isPaused && !move.moveBlocked && upgrades.hasSword && !move.delayedIsDashing){
				if(canFollowUpGate){
					anim.SetBool("CanFollowUp", true);
				}
				anim.SetBool("SwordAttacking", true);

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
	    if(Input.GetKeyDown(controls.keys["leftWeaponMenu"]) && !FindObjectOfType<PauseMenu>().isPaused && upgrades.wepMan.totalLeftWeapons > 0){
		    upgrades.wepMan.SelectNextWeaponL();
	    }
	    if(Input.GetKeyDown(controls.keys["rightWeaponMenu"]) && !FindObjectOfType<PauseMenu>().isPaused && upgrades.wepMan.totalRightWeapons > 0){
		    
		    int test = upgrades.wepMan.currentRightWeaponIndex+1;
		    if (test > upgrades.wepMan.totalRightWeapons){
		    	test = 0;
		    }
		    if(upgrades.wepMan.unlockedRightWeapon[test] != "HomingMissiles"){
			    homingMissileVolume.SetActive(false);
			    homingMissileVolume.GetComponent<HomingMissileTracking>().ClearList();
			    homingMissileSpacingGate = true;
			    homingMissileSpacingGate2 = true;
			    if(upgrades.wepMan.unlockedRightWeapon[test] != "None"){
			    	anim.SetBool("HasArmWeapon", true);
			    	anim.SetLayerWeight(1, 1f);
			    }
			    
			    
		    }
		    else{
		    	anim.SetBool("HasArmWeapon", false);
		    	anim.SetLayerWeight(1, 0f);
		    	homingMissileVolume.SetActive(true);
		    	homingMissileVolume.GetComponent<HomingMissileTracking>().ClearList();	
		    	
		    }
		    upgrades.wepMan.SelectNextWeaponR();

	    }
    }
}
