﻿using System;
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
    UpdateRotation rot;
    public Controls controls;
    float movementZ;
    float movementX;
    [SerializeField]
    [Tooltip("how quickly the animator blender between different directions when strafing")]
	//float XZBlend = 100f;

    public void JumpAnimEvent(){
		sphere.JumpTrigger(1f, true);
	}
    public void HighJumpAnimEvent(){
		sphere.JumpTrigger(1.2f, false);
	}

    void Start() {
        controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
        rot = player.GetComponentInChildren<UpdateRotation>();
        body = player.GetComponent<Rigidbody>();
        speed = player.GetComponent<MovementSpeedController>(); 
        sphere = player.GetComponent<Movement>();
        animator = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
		isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        onGroundHash = Animator.StringToHash("OnGround");
        isOnWallHash = Animator.StringToHash("isOnWall");
        isFallingHash = Animator.StringToHash("isFalling");
        isWalkingHash = Animator.StringToHash("isWalking");
        isAimingHash = Animator.StringToHash("isAiming");
        movementZHash = Animator.StringToHash("Movement Z");
        movementXHash = Animator.StringToHash("Movement X");
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

    void Update() {
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
        bool forwardPressed = Input.GetKey(sphere.controls.keys["walkUp"]);
        bool leftPressed = Input.GetKey(sphere.controls.keys["walkLeft"]);
        bool rightPressed = Input.GetKey(sphere.controls.keys["walkRight"]);
        bool backPressed = Input.GetKey(sphere.controls.keys["walkDown"]);
        bool movementPressed = forwardPressed || leftPressed || rightPressed || backPressed;
        if(rot.isAiming){
	        animator.SetBool(isAimingHash, true);
	        animator.SetLayerWeight(1, 1f);
        }
        else{
	        animator.SetBool(isAimingHash, false);
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
        }
    }

}
