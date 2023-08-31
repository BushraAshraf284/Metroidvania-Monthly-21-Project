using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

//I need to properly use hashes, im kinda half assing it here

public class AnimationStateController : MonoBehaviour
{
    public GameObject player = default;
    Movement sphere = default; 
    Animator animator;
	int isRunningHash;
    int isOnSteepHash;
    int isJumpingHash;
    int onGroundHash;
    int isOnWallHash;
    int isWalkingHash;
    int isFallingHash;
    int speedHash;
    int isAimingHash;
    int movementZHash;
    int movementXHash;
    int hasArmWeaponHash;
    int hasShockSpikeHash;
    int hasMissileHash;
    int dashingHash;
    int canFollowUpHash;
    int swordAttackingHash;

    bool isOnGround;

    [HideInInspector]
    public bool isOnGroundADJ;
    bool isOnSteep;
    //bool isOnSteepADJ;
    bool JumpPressed;
    [SerializeField]
    [Tooltip("how long you need to be in the air before the 'onGround' bool triggers")]
    float OnGroundBuffer = .5f;
    [SerializeField]
    [Tooltip("how long isJumping stays true after pressing it")]
    float JumpBuffer = .5f;
    bool JumpSwitch = true;
    float Groundstopwatch = 0;
    float Jumpstopwatch = 0;
    MovementSpeedController speed;
    Rigidbody body;
    float speedometer;
    public Controls controls;
    float movementZ;
    float movementX;
    [SerializeField]
	abilities abilities;
    public bool movementPressed;
    private string lastAnimationName = "";
    private bool noAnimationMessagePrinted = false;
    public bool enableDebugMessages = true;
	//bool dashingGate = false;
	bool jumpHeld;
	float jumpHeldTimer;
	[SerializeField]
	[Tooltip("How long you need to hold space for for vertical boost")]
	float jumpHeldCap = 3f;
	public bool canHighJump;
    public void ShootMissile(){
        abilities.fireMissile();
    }
    public void resetProngingAnimEvent(){
        animator.SetBool("Pronging", false);
    }
    public void blockMovementAnimEvent(){
		sphere.blockMovement();
	}
	public void unBlockMovementAnimEvent(){
		sphere.unblockMovement();
	}
    public void SpawnShockProngHitboxAnimEvent(){
		abilities.SpawnShockProngHitbox();
	}
	public void DeSpawnShockProngHitboxAnimEvent(){
		abilities.DeSpawnShockProngHitbox();
	}
    public void ShootSpike(){
        abilities.fireSpike();
    }
    public void JumpAnimEvent(){
	    sphere.JumpTrigger(1f, true);
	    jumpHeldTimer = 0f;
	}
    public void DashJumpAnimEvent(){
	    sphere.JumpTrigger(1f, true);
	    jumpHeldTimer = 0f;
	}
    public void HighJumpAnimEvent(){
	    sphere.JumpTrigger(1f, false);
	    jumpHeldTimer = 0f;
    }
	public void VertBoostJumpAnimEvent(){
		sphere.JumpTrigger(4f, false);
		canHighJump = false;
		jumpHeldTimer = 0f;
		animator.SetBool("VerticalBoost", false);
	}
    public void SetCanFollowUpGateTrueAnimEvent(){
        abilities.SetCanFollowUpGateTrue();
    }
    public void SetCanFollowUpGateFalseAnimEvent(){
        abilities.SetCanFollowUpGateFalse();
        SetSwordAttackingFalseAnimEvent();
    } 
    public void SetSwordAttackingFalseAnimEvent(){
        animator.SetBool(swordAttackingHash, false);
    }  

	void Start() {
		
        controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
        body = player.GetComponent<Rigidbody>();
        speed = player.GetComponent<MovementSpeedController>(); 
        sphere = player.GetComponent<Movement>();
        animator = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
        hasArmWeaponHash = Animator.StringToHash("HasArmWeapon");
		isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        onGroundHash = Animator.StringToHash("OnGround");
        isOnWallHash = Animator.StringToHash("isOnWall");
        isFallingHash = Animator.StringToHash("isFalling");
        isWalkingHash = Animator.StringToHash("isWalking");
        isAimingHash = Animator.StringToHash("isAiming");
        movementZHash = Animator.StringToHash("Movement Z");
        movementXHash = Animator.StringToHash("Movement X");
        hasMissileHash = Animator.StringToHash("HasMissile");
        hasShockSpikeHash = Animator.StringToHash("HasShockSpike");
        dashingHash = Animator.StringToHash("Dash");
        canFollowUpHash = Animator.StringToHash("CanFollowUp");
        swordAttackingHash = Animator.StringToHash("SwordAttacking");
    }

    //this is meant to allow a sort of buffer, so bools stay true for a set amount of time
    void BoolAdjuster(){
        isOnGround = sphere.OnGround;
        isOnSteep = sphere.OnSteep;
        if (!isOnGround && !JumpPressed){
            Groundstopwatch += Time.deltaTime;
            if (Groundstopwatch >= OnGroundBuffer){
                isOnGroundADJ = false;
            }
        }
        if (!isOnGround && JumpPressed){
            isOnGroundADJ = false;
        }
        if(isOnGround){
            Groundstopwatch = 0;
            isOnGroundADJ = true;
        }
    }
    float jumpCount;
    float jumpCap = .2f;
    public void resetDashingHash(){
        animator.SetBool(dashingHash, false);
    }
	// void resetDashingGate(){
	    //    dashingGate = false;
	    //}
    public void animDashTrigger(){
        abilities.Dash();
    }
    public void enableSwordHitbox(){
        abilities.swordHitbox.SetActive(true);
    }
    public void disableSwordHitbox(){
        abilities.swordHitbox.SetActive(false);
    }

	void Update() {
		jumpHeld = Input.GetKey(sphere.controls.keys["jump"]) && !FindObjectOfType<PauseMenu>().isPaused && !sphere.moveBlocked;
		if(jumpHeld && !canHighJump && isOnGround){
			if(jumpHeldTimer < jumpHeldCap){
				jumpHeldTimer += Time.deltaTime;
				
			}
			else{
				canHighJump = true;
				jumpHeldTimer = 0f;
			}
		}
	    speedometer = body.velocity.magnitude / speed.baseSpeed;
        animator.SetFloat(speedHash, speedometer, .1f, Time.deltaTime);
        
	    if(animator.GetFloat(speedHash) < .001f) {
		    animator.SetFloat(speedHash, 0f);
	    }
	    else if( animator.GetFloat(speedHash) > .980){
		    animator.SetFloat(speedHash, 1f);
	    }
        

	    animator.SetFloat(movementZHash, (Input.GetKey(controls.keys["walkUp"]) ? 1 : 0) - (Input.GetKey(controls.keys["walkDown"]) ? 1 : 0), .05f, Time.deltaTime); //this should be the forward back axis
	    animator.SetFloat(movementXHash, (Input.GetKey(controls.keys["walkRight"]) ? 1 : 0) - (Input.GetKey(controls.keys["walkLeft"]) ? 1 : 0), .05f, Time.deltaTime); //this should be the left right axis
           // animator.SetFloat(movementZHash, (Input.GetKey(controls.keys["walkUp"]) ? 1 : 0) - (Input.GetKey(controls.keys["walkDown"]) ? 1 : 0)); //this should be the forward back axis
           // animator.SetFloat(movementXHash, (Input.GetKey(controls.keys["walkRight"])? 1 : 0) - (Input.GetKey(controls.keys["walkLeft"])? 1: 0)); //this should be the left right axis
        
        if(animator.GetFloat(movementXHash) > .9f){
            animator.SetFloat(movementXHash, 1f);
        }
        if(animator.GetFloat(movementXHash) < -.9f){
            animator.SetFloat(movementXHash, -1f);
        }
        else if( animator.GetFloat(movementXHash) < .05f && animator.GetFloat(movementXHash) > -.05f){
            animator.SetFloat(movementXHash, 0f);
        }
        if(animator.GetFloat(movementZHash) > .9f){
            animator.SetFloat(movementZHash, 1f);
        }
        if(animator.GetFloat(movementZHash) < -.9f){
            animator.SetFloat(movementZHash, -1f);
        }
        else if( animator.GetFloat(movementZHash) < .05f && animator.GetFloat(movementZHash) > -.05f){
            animator.SetFloat(movementZHash, 0f);
        }


        BoolAdjuster();
		bool JumpPressed = Input.GetKeyDown(sphere.controls.keys["jump"]) && !FindObjectOfType<PauseMenu>().isPaused && !sphere.moveBlocked;
		
        
        isOnGround = isOnGroundADJ;
        bool isFalling = animator.GetBool(isFallingHash);
        bool isOnWall = animator.GetBool(isOnWallHash);
		bool isRunning = animator.GetBool(isRunningHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isWalking = animator.GetBool(isWalkingHash);
		bool forwardPressed = Input.GetKey(sphere.controls.keys["walkUp"]) && !Input.GetKey(sphere.controls.keys["walkDown"]);
		bool leftPressed = Input.GetKey(sphere.controls.keys["walkLeft"]) && !Input.GetKey(sphere.controls.keys["walkRight"]);
		bool rightPressed = Input.GetKey(sphere.controls.keys["walkRight"]) && !Input.GetKey(sphere.controls.keys["walkLeft"]);
		bool backPressed = Input.GetKey(sphere.controls.keys["walkDown"]) && !Input.GetKey(sphere.controls.keys["walkUp"]);
		movementPressed = forwardPressed||leftPressed||rightPressed||backPressed;
		//Debug.Log(movementPressed + " " + forwardPressed + " " + leftPressed + " " + rightPressed + " " + backPressed);
        if(abilities.upgrades.hasMissiles || abilities.upgrades.hasShockSpike){
            animator.SetBool(hasArmWeaponHash, true);
        }
        else{
            animator.SetBool(hasArmWeaponHash, false);
        }
        if(abilities.upgrades.hasMissiles){
            animator.SetBool(hasMissileHash, true);
            animator.SetBool(hasShockSpikeHash, false);
        }
        if(abilities.upgrades.hasShockSpike){
            animator.SetBool(hasShockSpikeHash, true);
            animator.SetBool(hasMissileHash, false);
        }
		if(abilities.isAiming ){
			//Debug.Log("IsAiminG!");
			if((abilities.upgrades.hasMissiles || abilities.upgrades.hasShockSpike)){
				//Debug.Log("Setting layer weight to 1 in in statement #1 in anim state controller");
				animator.SetLayerWeight(1, 1f);		
			}

        }
        if(abilities.isAiming){
	        animator.SetBool(isAimingHash, true);
        }
        else{
	        animator.SetBool(isAimingHash, false);
	        //Debug.Log("Setting layer weight to 0 in in statement #2 in anim state controller");
	        animator.SetLayerWeight(1, 0f);
        }
        if (isOnGround){
            animator.SetBool(onGroundHash, true);
        }
        else if (!isOnGround){
            animator.SetBool(onGroundHash, false);
        }
        //This makes jump stay true a little longer after you press it, dependent on "JumpBuffer"
        if (JumpPressed){
            if(JumpSwitch){
                Jumpstopwatch = 0;
                animator.SetBool(isJumpingHash, true);
                JumpSwitch = false;
            }
            else{
                Jumpstopwatch += Time.deltaTime;
                    if(Jumpstopwatch >= JumpBuffer){
                        animator.SetBool(isJumpingHash, false);
                    }
            }   
        }
        //this activates when jump is not pressed, counts until jumpbuffer, then disables jump
        if(!JumpPressed){
            JumpSwitch = true;
            Jumpstopwatch += Time.deltaTime;
            if(Jumpstopwatch >= JumpBuffer){
                animator.SetBool(isJumpingHash, false);
            }
        }
        
        // if you are in the air, adding timer to give a little time before the falling animation plays
        if (!isOnGroundADJ && !isOnSteep){
            jumpCount += Time.deltaTime;
            if(jumpCount > jumpCap){
                animator.SetBool(isFallingHash, true);
                animator.SetBool(isRunningHash, false);
                jumpCount = 0f;
            }

        }
        else if(isOnGroundADJ || isOnSteep){
            jumpCount = 0f;
        }

        else if (!isOnGroundADJ && isOnSteep){
            animator.SetBool(isOnWallHash, true);
        }

        if (isOnGroundADJ){
            animator.SetBool(isFallingHash, false);
            animator.SetBool(isOnWallHash, false);
        }

        if (isOnSteep){
            animator.SetBool("isOnSteep", true);
        }

        if (!isOnSteep){
            animator.SetBool("isOnSteep", false);
            animator.SetBool(isOnWallHash, false);
        }

        if ((!isRunning && movementPressed && sphere.velocity.magnitude > 0 )&& !speed.slowed){
            animator.SetBool(isRunningHash, true);
        }
        if (((isRunning && !movementPressed) || sphere.velocity.magnitude <= 0.08f ) && !speed.slowed){
            animator.SetBool(isRunningHash, false);
        }
        if(speed.slowed && isOnGround && movementPressed && sphere.velocity.magnitude > 0) {
            animator.SetBool(isWalkingHash, true);
        }
        if (((isWalking && !movementPressed) || sphere.velocity.magnitude <= 0.01f )){
            animator.SetBool(isWalkingHash, false);
        }
        if(!movementPressed){
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
            animator.SetFloat(speedHash, 0f);
        }
        
        LogCurrentAnimation();
    }

    private void LogCurrentAnimation()
    {
        if (!enableDebugMessages)
        {
            return;
        }

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            noAnimationMessagePrinted = false;
            string currentAnimationName = clipInfo[0].clip.name;

            if (currentAnimationName != lastAnimationName)
            {
                Debug.Log("Current Animation: " + currentAnimationName);
                lastAnimationName = currentAnimationName;
            }
        }
        else
        {
            if (!noAnimationMessagePrinted)
            {
                Debug.Log("No animation clip is currently playing.");
                noAnimationMessagePrinted = true;
            }
        } 
    }

}
